using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Common
{
    public class Sort
    {
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
        public Sort(string orderBy, string sortOrder)
        {
            OrderBy = orderBy;
            SortOrder = sortOrder;
        }
    }
}
