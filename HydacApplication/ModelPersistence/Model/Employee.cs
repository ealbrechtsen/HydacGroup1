using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName;
        public string LastName;
        public string CPRNum;
        public string PhoneNum;
        public string Email;
        public string Address;
        public bool EmploymentStatus;
        public Department Department;
        public KeyChip KeyChip;
        
        public Employee(string firstName, string lastName, string cPRNum, string phoneNum, string email, string address, Department department)
        {
            FirstName = firstName;
            LastName = lastName;
            CPRNum = cPRNum;
            PhoneNum = phoneNum;
            Email = email;
            Address = address;
            EmploymentStatus = true;
            Department = department;
        }
    }
}
