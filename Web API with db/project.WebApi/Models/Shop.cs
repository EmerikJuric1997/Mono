using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace project.WebApi.Models
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string ShopName { get; set; }
        public string ShopLocation { get; set; }
        public int AddressNumber { get; set; }

    }
}