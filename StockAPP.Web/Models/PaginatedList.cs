using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPP.Web.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalRecords { get; private set; }

        public PaginatedList(IList<T> items, int pageIndex, int pageSize)
        {
            TotalRecords = items.Count;
            var itemslist = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            this.AddRange(itemslist);
        }
    }
}
