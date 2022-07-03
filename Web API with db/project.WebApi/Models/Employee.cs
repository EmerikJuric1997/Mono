using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.WebApi.Models
{
    public class Employee
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }

    }
}