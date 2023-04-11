using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StockAPP.API.ExtendedModel;
using StockAPP.API.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using StockAPP.API.Repositories;

namespace StockAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IStockAPPDbContext _context;

        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IStockAPPDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        //Register method
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            var userExists = await _userManager.FindByNameAsync(user.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser newUser = new()
            {
                Email = user.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = user.Username
            };

            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (!isCreated.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = isCreated.ToString() });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        //Login method
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel usermodel)
        {
            var user = await _userManager.FindByNameAsync(usermodel.Username);
            if (user!= null && await _userManager.CheckPasswordAsync(user, usermodel.Password))
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                refreshToken.User_ID = user.Id;
                _context.RefreshToken.Add(refreshToken);
                await _context.SaveChangesAsync();

                var jwtToken = GenerateAccessToken(user.UserName);
                UserWithToken userWithToken = new UserWithToken(usermodel)
                {
                    RefreshToken = refreshToken.Token,
                    AccessToken = jwtToken
                };

                return Ok(userWithToken);
            }

            return Unauthorized();
        }

        // GET: api/Users
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            IdentityUser user = await GetUserFromAccessToken(refreshRequest.AccessToken);
            LoginModel userm = new LoginModel()
            {
                Username = user.UserName
            };

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(userm);
                userWithToken.AccessToken = GenerateAccessToken(user.Id);

                return userWithToken;
            }

            return null;
        }

        // GET: api/Users
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<IdentityUser>> GetUserByAccessToken([FromBody] string accessToken)
        {
            IdentityUser user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        //Generate token by authorization claim asp.net Identity
        private string GenerateAccessToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Issuer = _configuration["JWTSettings:ValidIssuer"],
                Audience = _configuration["JWTSettings:ValidAudience"],
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //Generate refresh token
        private static RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.Expiry_Date = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }

        //Validating Refresh Token by User
        private bool ValidateRefreshToken(IdentityUser user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _context.RefreshToken.Where(rt => rt.Token == refreshToken)
                                                .OrderByDescending(rt => rt.Expiry_Date)
                                                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.User_ID == user.Id
                && refreshTokenUser.Expiry_Date > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        //Get User from Access Token
        private async Task<IdentityUser> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    return await _userManager.FindByIdAsync(userId);
                }
            }
            catch (Exception)
            {

                return new IdentityUser();
            }

            return new IdentityUser();
        }
    }
}
