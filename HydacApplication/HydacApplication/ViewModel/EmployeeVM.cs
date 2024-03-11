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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPRNum { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }

        public EmployeeVM(Employee employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            CPRNum = employee.CPRNum;
            PhoneNum = employee.PhoneNum;
            Email = employee.Email;
            Address = employee.Address;
            Department = employee.Department.Name;
        }
    }
}
