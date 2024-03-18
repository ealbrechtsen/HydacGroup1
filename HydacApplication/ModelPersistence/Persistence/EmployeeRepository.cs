using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;
using ModelPersistence.Model;
using ModelPersistence.Persistence;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ModelPersistence.Persistence
{
    public class EmployeeRepository
    {
        private List<Employee> employees;
        private DepartmentRepository departmentRepo;
        private KeyChipRepository keyChipRepo;

        public EmployeeRepository(DepartmentRepository departmentRepo, KeyChipRepository keyChipRepo)
        {
            // When the EmployeeRepository is instantiated, we take the Department repo and place it inside a field,
            // so we can use the functions in departmentrepo, to assign departments to employees.
            this.departmentRepo = departmentRepo;
            this.keyChipRepo = keyChipRepo;
            // Next we instantiate the list and populate it with objekt from the database.
            employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(RepositoryHelper.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeId, FirstName, LastName, DepartmentName, EmploymentStatus, KeyChipId FROM EMPLOYEE", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        (
                            int.Parse(reader["EmployeeId"].ToString()),
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            bool.Parse(reader["EmploymentStatus"].ToString()),
                            this.keyChipRepo.GetKeyChip(int.Parse(reader["KeyChipId"].ToString())),
                            // Here we use the departmentrepo to get a department objekt and assign it to the employee instead of just the name of the department.
                            this.departmentRepo.GetDepartment(reader["DepartmentName"].ToString())
                        );
                        employees.Add(employee);
                    }
                }
            }
        }
        public void Add(Employee employee)
        {
            // Adds an employee to the database and the list.
            using(SqlConnection connection = new SqlConnection(RepositoryHelper.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE(FirstName, LastName, EmploymentStatus, DepartmentName, KeyChipId)" + "VALUES(@FirstName, @LastName, @EmploymentStatus, @DepartmentName, @KeyChipId)", connection);
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = employee.LastName;
                cmd.Parameters.Add("@EmploymentStatus", SqlDbType.Bit).Value = employee.EmploymentStatus;
                cmd.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = employee.Department.Name;
                cmd.Parameters.Add("@KeyChipId", SqlDbType.Int).Value = employee.KeyChip.KeyChipId;
                cmd.ExecuteNonQuery();
                employees.Add(employee);

            }
        }
        public Employee GetEmployee(int id)
        {
            // Returns a Employee with the given name.
            return employees.Find(employee => employee.EmployeeId == id);
        }
        public List<Employee> GetEmployees()
        {
            // Returns the Department list.
            return employees;
        }
        public void UpdateStatus(Employee employee) 
        {
            // Flips the status
            employee.EmploymentStatus = employee.EmploymentStatus == true ? false : true;
            // Finds the first object matching with the employeeId. Then sets that employee.EmploymentStatus to the new employees .employmentStatus.
            // Only updates the local value in the list.
            employees.FirstOrDefault(emp => emp.EmployeeId == employee.EmployeeId).EmploymentStatus = employee.EmploymentStatus;
            // Next we update the Database
            using(SqlConnection connection = new SqlConnection(RepositoryHelper.connectionString))
            {
                connection.Open();
                // We use UPDATE table SET Column = @value WHERE column = @Value.
                // We want to UPDATE EmploymentStatus to the employee from the method WHERE EmployeeID matches with the employee from the method.
                SqlCommand cmd = new SqlCommand("UPDATE EMPLOYEE SET EmploymentStatus = @EmploymentStatus WHERE EmployeeId = @EmployeeId", connection);
                // Here we push the local values into the command statement.
                cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employee.EmployeeId;
                cmd.Parameters.Add("@EmploymentStatus", SqlDbType.Bit).Value = employee.EmploymentStatus;
                // Then we execute the command.
                cmd.ExecuteNonQuery();
            }
        }
    }
}
