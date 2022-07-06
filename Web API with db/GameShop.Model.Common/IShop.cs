using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Model.Common
{
    public interface IShop
    {
        Guid Id { get; set; }
        string ShopName { get; set; }
        string ShopLocation { get; set; }
        int AddressNumber { get; set; }
    }
}
