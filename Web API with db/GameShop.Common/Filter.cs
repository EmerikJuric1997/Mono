using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Common
{
    public class Filter
    {
        public string SearchName { get; set; }
        public string SearchLocation { get; set; }

        public Filter(string searchName, string searchLocation)
        {
            SearchName = searchName;
            SearchLocation = searchLocation;
        }
    }
}
