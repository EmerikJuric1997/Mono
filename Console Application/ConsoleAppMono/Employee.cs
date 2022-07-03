using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Encapsulation
class Employee
{
    public string EmployeFullName;
    private int _pay;
    public int Pay
    {
        get
        {
            return _pay;
        }
        set
        {
            _pay = value;
        }
    }

    public Employee(string eName, int ePay)
    {
        this.EmployeFullName = eName;
        this.Pay = ePay;
    }
}
