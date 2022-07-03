using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace project.WebApi.Models
{
    public class Game
    {
        public Guid Id;
        public string GameName;
        public string GameDescription;
        public int AgeRestriction;

    }
}