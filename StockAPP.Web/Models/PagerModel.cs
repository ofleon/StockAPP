using System;

namespace StockAPP.Web.Models
{
    public class PagerModel
    {
        //Read Only Properties
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartRecord { get; private set; }
        public int EndRecord { get; private set; }

        //public properties
        public string Action { get; set; } = "Index";
        public string Searchtext { get; set; }
        public string SortExpression { get; set; }

        public PagerModel(int totalItems, int currentPage, int pageSize=5)
        {
            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            TotalPages = totalPages;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                    startPage = endPage - 9;
            }
            StartRecord = (CurrentPage - 1) * PageSize + 1;
            EndRecord = StartRecord - 1 + PageSize;

            if(TotalItems == 0)
            {
                StartPage = 0;
                StartRecord = 0;
                CurrentPage = 0;
                EndRecord = 0;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }
    }
}
