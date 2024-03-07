using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class Employee
    {
        public Department? department;
        public bool EmploymentStatus;
        public string? FirstName;
        public string? LastName;
        public string? CPRNum;
        public string? PhoneNum;
        public string? Email;
        public string? Address;
        
        public Employee(string? FirstName, string? LastName, string? CPRNum, string? PhoneNum, string? Email, string? Address)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.CPRNum = CPRNum;
            this.PhoneNum = PhoneNum;
            this.Email = Email;
            this.Address = Address;
        }
        public void Set(Department department)
        {

        }
    }
}
