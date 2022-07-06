using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using GameShop.Model.Common;

namespace ShopModel
{
    public class Shop : IShop
    {
        public Guid Id { get; set; }
        public string ShopName { get; set; }
        public string ShopLocation { get; set; }
        public int AddressNumber { get; set; }

        public Shop(Guid id, string name, string location, int address)
        {
            this.Id = id;
            this.ShopName = name;
            this.ShopLocation = location;
            this.AddressNumber = address;
        }
        public Shop(string name, string location, int address)
        {
            this.ShopName = name;
            this.ShopLocation = location;
            this.AddressNumber = address;
        }
        public Shop()
        {
        }
    }
}
