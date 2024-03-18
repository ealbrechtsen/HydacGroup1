using ModelPersistence.Model;
using ModelPersistence.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydacApplication.ViewModel
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmploymentStatus { get; set; }
        public KeyChip KeyChip { get; set; }
        public Department Department { get; set; }

        // Constructor for reading from database
        public EmployeeVM(Employee employee)
        {
            EmployeeId = employee.EmployeeId;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            EmploymentStatus = employee.EmploymentStatus;
            Department = employee.Department;
            KeyChip = employee.KeyChip;
        }
        public Employee GetEmployee(EmployeeRepository repo) // uses the repository to get the original non vm object, based on the Id of the current Vm object.
        {
            return repo.GetEmployee(EmployeeId);
        }
    }
}
