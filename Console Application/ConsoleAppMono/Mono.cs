using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Inheritance, Polymorphism
class Mono : ITFirm
{
    public override void AboutTheFirm()
    {
        Console.WriteLine(Employes + " people currently work at " + FirmName + ". We are located in: " + Location);
    }
}

