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
        public int EmployeeId;
        public string FirstName;
        public string LastName;
        public KeyChip KeyChip { get; set; }
        public Department Department { get; set; }

        // Constructor for reading from database
        public EmployeeVM(Employee employee)
        {
            EmployeeId = employee.EmployeeId;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Department = employee.Department;
            KeyChip = employee.KeyChip;
        }
    }
}
