using System;
using project.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;

namespace project.WebApi.Controllers
{
    public class ShopController : ApiController
    {

        public string connstr = "Data Source=DESKTOP-BBAM4GR\\SQLEXPRESS;Initial Catalog=MonoDB;Integrated Security=True";

        // GET: api/Shop
        public HttpResponseMessage GetShops()
        {

            var conn = new SqlConnection(connstr);
            using (conn)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT * FROM Shop;",
                  conn);
                conn.Open();
                List<Shop> shops = new List<Shop>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var shop = new Shop
                        {
                            Id = reader.GetGuid(0),
                            ShopName = reader.GetString(1),
                            ShopLocation = reader.GetString(2),
                            AddressNumber = reader.GetInt32(3)
                        };
                        shops.Add(shop);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error!");
                }
                reader.Close();

                return Request.CreateResponse(HttpStatusCode.OK, shops);
            }
        }

        [Route("api/Shop/{id}")]
        // GET: api/Shop/5
        public HttpResponseMessage GetShop(Guid id)
        {
            SqlConnection connection = new SqlConnection(connstr);
            using (connection)
            {
                Shop shop = new Shop();
                SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Shop WHERE Id = @ID", connection);
                cmd.Parameters.AddWithValue("@ID", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        shop.Id = reader.GetGuid(0);
                    }
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, shop);
                }
                catch (SqlException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
                }
            }
        }




        // POST: api/Shop
        public HttpResponseMessage AddNewShop([FromBody] Shop value)
        {
            {
                SqlConnection conn = new SqlConnection(connstr);
                Guid id = System.Guid.NewGuid();
                using (conn)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"INSERT INTO Shop(Id, ShopName, ShopLocation, AddressNumber) 
                                            VALUES(@ID,@NAME,@LOCATION,@ADDRESS)";

                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@NAME", value.ShopName);
                        cmd.Parameters.AddWithValue("@LOCATION", value.ShopLocation);
                        cmd.Parameters.AddWithValue("@ADDRESS", value.AddressNumber);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            return Request.CreateResponse(HttpStatusCode.OK, "You have added a new shop!");
                        }
                        catch (SqlException)
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
                        }

                    }
                }
            }

        }

        [HttpPut]
        [Route("api/Shop/update/{id}")]

        //PUT: api/Shop/5
        public HttpResponseMessage Put(Guid id, [FromBody] Shop value)
        {
            var conn = new SqlConnection(connstr);
            using (conn)
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE Shop SET ShopName = @NAME, ShopLocation = @LOCATION, AddressNumber = @ADDRESS WHERE Id = @ID;";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@NAME", value.ShopName);
                    cmd.Parameters.AddWithValue("@LOCATION", value.ShopLocation);
                    cmd.Parameters.AddWithValue("@ADDRESS", value.AddressNumber);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return Request.CreateResponse(HttpStatusCode.OK, "You have updated a shop!");
                    }
                    catch (SqlException)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
                    }

                }

            }

        }

        [Route("api/shop/delete/{id}")]
        // DELETE: api/Shop/5
        public HttpResponseMessage Delete(Guid id)
        {

            var conn = new SqlConnection(connstr);
            using (conn)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"DELETE FROM Shop WHERE Id = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return Request.CreateResponse(HttpStatusCode.OK, "You have deleted a shop!");
                    }
                    catch (SqlException)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
                    }

                }

            }
        }
    }
}
