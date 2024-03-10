using ModelPersistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydacApplication.ViewModel
{
    public class EmployeeVM
    {
        public string FirstName;
        public string LastName;
        public string CPRNum;
        public string PhoneNum;
        public string Email;
        public string Address;
        public bool EmploymentStatus;
        public Department Department;

        public EmployeeVM(Employee employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            CPRNum = employee.CPRNum;
            PhoneNum = employee.PhoneNum;
            Email = employee.Email;
            Address = employee.Address;
            EmploymentStatus = employee.EmploymentStatus;
            Department = employee.Department;
        }
    }
}
