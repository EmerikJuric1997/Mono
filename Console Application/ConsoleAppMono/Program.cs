using System;
using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {

            AverageSalary placa = new AverageSalary();
            Mono mono = new Mono();
            mono.FirmName = "Mono";
            mono.Location = "Osijek";
            mono.Employes = 100;
            mono.AboutTheFirm();
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee("Marko Marić", 8000));
            employees.Add(new Employee("Pero Peroć", 6500));
            foreach (Employee e in employees)
            {
                Console.WriteLine(e.EmployeFullName + " has a paycheck of " + e.Pay);
            }
            placa.SalaryAverage(5000, 9500);
        
        }
    }
