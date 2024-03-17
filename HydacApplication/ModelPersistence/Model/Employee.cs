using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class Employee
    {
        public int EmployeeId;
        public string FirstName;
        public string LastName;
        public bool EmploymentStatus {  get; set; }
        public KeyChip KeyChip { get; set; }
        public Department Department { get; set; }
        private int count = 1;
        
        // Constructor for reading from database
        public Employee(int employeeId, string firstName, string lastName, bool employmentStatus, KeyChip keyChip, Department department)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            EmploymentStatus = employmentStatus;
            KeyChip = keyChip;
            Department = department;
            count++; // for local Id simulations. We use it to assign correct ID's to newly added Employees.
        }

        // Constructor for adding new employee
        public Employee(string firstName, string lastName, KeyChip keyChip, Department department)
        {
            EmployeeId = count;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            EmploymentStatus = true;
            KeyChip = keyChip;
            Department = department;
            count++;
        }
    }
}
