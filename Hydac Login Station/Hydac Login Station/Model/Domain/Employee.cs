using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydac_Login_Station.Model.Domain
{
    public class Employee
    {
        public int EmployeeId;
        public string FirstName;
        public string LastName;
        public long KeyChip;

        // Constructor for adding already existing employees read from the database
        public Employee(int employeeId, string firstName, string lastName, long keyChip)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            KeyChip = keyChip;
        }
    }
}
