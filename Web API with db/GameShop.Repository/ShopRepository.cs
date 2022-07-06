using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using ShopModel;
using ShopGameRepository.Common;
using System.Net.Http;
using GameShop.Common;
using System.Linq;
using System.Text;

namespace ShopRepositoryRepository
{
    public class ShopRepository : IShopRepository
    {

        public string connectionstring = "Data Source=DESKTOP-BBAM4GR\\SQLEXPRESS;Initial Catalog=MonoDB;Integrated Security=True; MultipleActiveResultSets=true";
        public async Task<List<Shop>> GetAllShopsAsync(Pagination pagination, Filter filter, Sort sort)
        {
            int offset = (pagination.PageNumber - 1) * pagination.PerPage;
            StringBuilder builder = new StringBuilder("SELECT * FROM Shop WHERE 1=1 ");
            if (filter.SearchName != null)
            {
                builder.Append(string.Format("AND ShopName LIKE {0} ", filter.SearchName));
            }
            if (filter.SearchLocation != null)
            {
                builder.Append(string.Format("AND ShopLocation LIKE {0} ", filter.SearchLocation));
            }
            builder.Append(string.Format("ORDER BY {0} {1} ", sort.OrderBy, sort.SortOrder));
            builder.Append("offset @offset ROWS fetch next @perPage ROWS ONLY");
            var conn = new SqlConnection(connectionstring);
            using (conn)
            {
                SqlCommand command = new SqlCommand(
                  builder.ToString(),
                  conn);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@perPage", pagination.PerPage);
                conn.Open();
                List<Shop> shops = new List<Shop>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        shops.Add(new Shop(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                        /*{
                            Id = reader.GetGuid(0),
                            ShopName = reader.GetString(1),
                            ShopLocation = reader.GetString(2),
                            AddressNumber = reader.GetInt32(3),
                            
                    };*/
                        //return shops;
                        
                    }   
                }
                else
                {
                    reader.Close();
                    throw new Exception("No result!");
                }
                reader.Close();
                return shops;
                

            }
        }

        public async Task<Shop> GetOneShopAsync(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            using (conn)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    Shop shop = new Shop();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * FROM Shop WHERE Id = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                  
                    try
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                shop.Id = reader.GetGuid(0);
                                shop.ShopName = reader.GetString(1);
                                shop.ShopLocation = reader.GetString(2);
                                shop.AddressNumber = reader.GetInt32(3);
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        throw new Exception("Error!");
                    }
                    reader.Close();
                    return shop;
                }
            }
        }

        public Shop AddNewShop(Shop value)
        {
            {
                SqlConnection conn = new SqlConnection(connectionstring); ;
                Guid id = Guid.NewGuid();
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
                        Shop newShop = new Shop(id, value.ShopName, value.ShopLocation, value.AddressNumber);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            return newShop;
                        }
                        catch (SqlException)
                        {
                            throw new Exception("Error!");
                        }

                    }
                }
            }

        }

        public Shop Put(Guid id, Shop value)
        {
            var conn = new SqlConnection(connectionstring);
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
                    Shop newShop = new Shop(id, value.ShopName, value.ShopLocation, value.AddressNumber);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return newShop;
                    }
                    catch (SqlException)
                    {
                        throw new Exception("Error!");
                    }

                }

            }

        }

        public async Task<string> DeleteAsync(Guid id)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                 SqlCommand cmd = new SqlCommand("DELETE FROM Shop WHERE Id = @ID", conn);
                 cmd.Parameters.AddWithValue("@ID", id);
                 conn.Open();
                 await cmd.ExecuteNonQueryAsync();
                 return "You have deleted the shop!";
            }
        }
    }
}