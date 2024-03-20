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
        public bool EmploymentStatus;
        public KeyChip KeyChip;
        public Department Department;
        private int count = 1; // Simulates auto incrementation of Database, so newly added employees have correct corrosponding EmployeeId's
        
        // Constructor for adding already existing employees read from the database
        public Employee(int employeeId, string firstName, string lastName, bool employmentStatus, KeyChip keyChip, Department department)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            EmploymentStatus = employmentStatus;
            KeyChip = keyChip;
            Department = department;
            count++; // increments based on number of employees added from database.
        }

        // Constructor for adding new employees
        public Employee(string firstName, string lastName, KeyChip keyChip, Department department)
        {
            EmployeeId = count; // uses the auto increment field to set the correct Id count for the next added employee.
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            EmploymentStatus = true;
            KeyChip = keyChip;
            Department = department;
            count++; // increments based on number of new employees added.
        }
    }
}
