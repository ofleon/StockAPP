using System.ComponentModel.DataAnnotations;

namespace StockAPP.API.ExtendedModel
{
    public class ItemModel
    {
        [Required(ErrorMessage = "Item Code is required")]
        public string Item_Code { get; set; }

        [Required(ErrorMessage = "FNSKU is required")]
        public string FNSKU { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
