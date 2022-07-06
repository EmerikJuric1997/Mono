using System;
using ShopModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ShopServiceCommon.Common;
using GameShop.Common;
using System.Linq;

namespace project.WebApi.Controllers
{
    public class ShopController : ApiController
    {
        private IShopService _service;
        public ShopController(IShopService objShopService)
        {
            _service = objShopService;
        }

        [HttpGet]
        [Route("api/allshops")]
        // GET: api/Shop
        public async Task<HttpResponseMessage> GetAllShopsAsync(int perPage = 3, int pageNumber = 1, string searchName = null, string searchLocation =null, string orderBy = "ShopName", string sortOrder = "ASC")
        {
            //List<Shop> shops = new List<Shop>();
            List<ShopRest> shopsRest = new List<ShopRest>();
            var shops = await _service.GetAllShopsAsync(new Pagination(perPage, pageNumber), new Filter(searchName, searchLocation), new Sort(orderBy, sortOrder));
            
            if (shops == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Empty database!");
            }

            else
            {
                foreach (Shop shop in shops)
                {
                    shopsRest.Add(new ShopRest(shop.Id, shop.ShopName, shop.ShopLocation, shop.AddressNumber));
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, shopsRest);

        }

        [HttpGet]
        [Route("api/shopbyid/{id}")]
        // GET: api/Shop/5
        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            {
                var shop = await _service.GetOneShopAsync(id);
                if (shop == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No book with that ID in database.");
                }

                else
                {
                    ShopRest shopById = new ShopRest(shop.Id, shop.ShopName, shop.ShopLocation, shop.AddressNumber);
                    return Request.CreateResponse(HttpStatusCode.OK, shop);
                }

            }
        }
        [HttpPost]
        [Route("api/addnewshop")]
        // POST: api/Shop
        public HttpResponseMessage Post([FromBody] ShopRest value)
        {
            Shop shop = new Shop(value.Id, value.ShopName, value.ShopLocation, value.AddressNumber);

            var result =  _service.AddNewShop(shop);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Something went wrong.");
            }

            else return Request.CreateResponse(HttpStatusCode.OK, shop);

        }

        //PUT: api/Shop/5
        [HttpPut]
        [Route("api/updateshop/{id}")]
        public HttpResponseMessage Put(Guid id, [FromBody] Shop value)
        {
             var result = _service.Put(id, value);
             if (result == null)
             {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
             }

             else return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        // DELETE: api/Shop/5
        [HttpDelete]
        [Route("api/deleteshop/{id}")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
             var result = await _service.DeleteAsync(id);
             if (result == null)
             {
                 return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
             }

             else return Request.CreateResponse(HttpStatusCode.OK, result);
        }
          

        public class ShopRest
        {

            public Guid Id { get; set; }
            public string ShopName { get; set; }
            public string ShopLocation { get; set; }
            public int AddressNumber { get; set; }

            public ShopRest(Guid id, string name, string location, int address)
            {
                this.Id = id;
                this.ShopName = name;
                this.ShopLocation = location;
                this.AddressNumber = address;
            }


        }
    }
}
