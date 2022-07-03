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
    public class GameController : ApiController
    {

        public string connstr = "Data Source=DESKTOP-BBAM4GR\\SQLEXPRESS;Initial Catalog=MonoDB;Integrated Security=True";
        [Route("api/Game")]
        // GET: api/Game
        public HttpResponseMessage GetGames()
        {

            var conn = new SqlConnection(connstr);
            using (conn)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT * FROM Game;",
                  conn);
                conn.Open();
                List<Game> games = new List<Game>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var game = new Game
                        {
                            Id = reader.GetGuid(0),
                            GameName = reader.GetString(1),
                            GameDescription = reader.GetString(2),
                            AgeRestriction = reader.GetInt32(3)
                        };
                        games.Add(game);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error!");
                }
                reader.Close();

                return Request.CreateResponse(HttpStatusCode.OK, games);
            }
        }

        [Route("api/Game/{id}")]
        // GET: api/Game/5
        public HttpResponseMessage GetG(Guid id)
        {
            SqlConnection conn = new SqlConnection(connstr);
            using (conn)
            {
                Game game = new Game();
                SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Game WHERE Id = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        game.Id = reader.GetGuid(0);
                    }
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, game);
                }

                catch (SqlException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
                }
            }
        }



        [Route("api/Game")]
        // POST: api/Game
        public HttpResponseMessage AddNewGame([FromBody] Game value)
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
                        cmd.CommandText = @"INSERT INTO Game(Id, GameName, GameDescription, AgeRestriction) 
                                            VALUES(@ID,@NAME,@DESCRIPTION,@AGERESTRICTION)";

                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@NAME", value.GameName);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", value.GameDescription);
                        cmd.Parameters.AddWithValue("@AGERESTRICTION", value.AgeRestriction);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            return Request.CreateResponse(HttpStatusCode.OK, "You have added a new game!");
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
        [Route("api/Game/update/{id}")]

        //PUT: api/Game/5
        public HttpResponseMessage UpdateGame(Guid id, [FromBody] Game value)
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
                        return Request.CreateResponse(HttpStatusCode.OK, "You have updated a shop!");
                    }
                    catch (SqlException)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
                    }

                }

            }

        }

        [Route("api/Game/delete/{id}")]
        // DELETE: api/Shop/5
        public HttpResponseMessage DeleteGame(Guid id)
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
                        return Request.CreateResponse(HttpStatusCode.OK, "You have deleted a game!");
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
