using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class AverageSalary : ISalary
{
    public void SalaryAverage(int LowestSalary, int HighestSalary)
    {

        int placa = (LowestSalary + HighestSalary) / 2;
        Console.WriteLine("Average salary in IT is: " + placa);
    }
}


