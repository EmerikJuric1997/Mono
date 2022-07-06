using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using GameModel;
using GameRepository.Common;

namespace GameShopRepository.Repository
{
    public class GameRepository : IGameRepository
    {
        public string connstr = "Data Source=DESKTOP-BBAM4GR\\SQLEXPRESS;Initial Catalog=MonoDB;Integrated Security=True; MultipleActiveResultSets=true";

        public async Task<List<Game>> GetAllGamesAsync()
        {
            var conn = new SqlConnection(connstr);
            using (conn)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT * FROM Game;",
                  conn);
                conn.Open();
                List<Game> shops = new List<Game>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var shop = new Game
                        {
                            Id = reader.GetGuid(0),
                            GameName = reader.GetString(1),
                            GameDescription = reader.GetString(2),
                            AgeRestriction = reader.GetInt32(3)
                        };
                        shops.Add(shop);
                    }
                }
                else
                {
                    throw new Exception("No result!");
                }
                reader.Close();

                return shops;

            }
        }

        public async Task<Game> GetOneGameAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connstr);
            using (connection)
            {
                Game game = new Game();
                SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Game WHERE Id = @ID", connection);
                cmd.Parameters.AddWithValue("@ID", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                //reader.ReadAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        game.Id = reader.GetGuid(0);
                    }
                }
                try
                {
                    //await
                    cmd.ExecuteNonQuery();
                    return game;
                }
                catch (SqlException)
                {
                    throw new Exception("Error!");
                }
            }
        }

        public Game AddNewGame(Game value)
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
                        cmd.CommandText = @"INSERT INTO Game(Id, ShopName, ShopLocation, AddressNumber) 
                                            VALUES(@ID,@NAME,@DESCRIPTION,@AGERESTRICTION)";

                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@NAME", value.GameName);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", value.GameDescription);
                        cmd.Parameters.AddWithValue("@AGERESTRICTION", value.AgeRestriction);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            throw new Exception("You have added a game");
                        }
                        catch (SqlException)
                        {
                            throw new Exception("Error!");
                        }

                    }
                }
            }

        }

        public Game Put(Guid id, Game value)
        {
            var conn = new SqlConnection(connstr);
            using (conn)
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE Game SET GameName = @NAME, GameDescription = @DESCRIPTION, AgeRestriction = @AGERESTRICTION WHERE Id = @ID;";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@NAME", value.GameName);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", value.GameDescription);
                    cmd.Parameters.AddWithValue("@AGERESTRICTION", value.AgeRestriction);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        throw new Exception("You have updated a game");
                    }
                    catch (SqlException)
                    {
                        throw new Exception("Error!");
                    }

                }

            }

        }

        public Game Delete(Guid id)
        {

            var conn = new SqlConnection(connstr);
            using (conn)
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"DELETE FROM Game WHERE Id = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        throw new Exception("You deleted a game!");
                    }
                    catch (SqlException)
                    {
                        throw new Exception("Error!");
                    }

                }

            }
        }
    }
}

