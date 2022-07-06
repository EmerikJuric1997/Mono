using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Common
{
    public class Pagination
    {
        public int PerPage;
        public int PageNumber;
        public Pagination(int perPage, int pageNumber)
        {
            this.PerPage = perPage;
            this.PageNumber = pageNumber;
        }
    }
}
