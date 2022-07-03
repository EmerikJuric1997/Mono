using project.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {

        public static List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, Name = "Marko", Surname = "Marić", Firm = "Mono" },
            new Employee { Id = 2, Name = "Luka", Surname = "Perić", Firm = "HEP" },
            new Employee { Id = 3, Name = "Matija", Surname = "Mavrović", Firm = "Mono" },
            new Employee { Id = 4, Name = "Marija", Surname = "Klarić", Firm = "Mono" },
            new Employee { Id = 5, Name = "Ana", Surname = "Sarić", Firm = "Vodovod" },
            new Employee { Id = 6, Name = "Klara", Surname = "Larić", Firm = "Mono" },
            new Employee { Id = 7, Name = "Vana", Surname = "Varić", Firm = "Mono" },
            new Employee { Id = 8, Name = "Ante", Surname = "Barić", Firm = "Mono" },
            new Employee { Id = 9, Name = "Mateja", Surname = "Narić", Firm = "Mono" },
            new Employee { Id = 10, Name = "Josipa", Surname = "Tarić", Firm = "Mono" },
            new Employee { Id = 11, Name = "Sandra", Surname = "Rarić", Firm = "Mono" }
        };

        // GET api/employee
        public IEnumerable<Employee> GetAll()
        {

            return employees;
        }

        // GET api/employee/5
        public Employee Get(int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }


        // POST api/employee
        [HttpPost]
        public IEnumerable<Employee> Post([FromBody] Employee value)
        {
            if (!employees.Equals(value.Id))
            {
                employees.Add(value);
                return employees;
            }
            else
            {
                return employees;
            }


        }

        // PUT api/employee/5
       // public bool Put([FromBody] Employee value)
       // {
        //    var list = employees;
         //   //employees.Where(p => p.Id == value.Id).(p => p.Name = value.Name);
         //   return true;

       // }

        // DELETE api/employee/5
        public IEnumerable<Employee> Delete(int id)
        {
            employees.RemoveAll(x => x.Id == id);
            return employees;
            
        }
    }
}
