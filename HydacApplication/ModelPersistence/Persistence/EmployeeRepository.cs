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
    public class EmployeeRepository : IRepository<Employee>
    {
        private List<Employee> employees;
        private DepartmentRepository departmentRepo;

        public EmployeeRepository(DepartmentRepository DepartmentRepo)
        {
            // When the EmployeeRepository is instantiated, we take the Department repo and place it inside a field,
            // so we can use the functions in departmentrepo, to assign departments to employees.
            departmentRepo = DepartmentRepo;
            // Next we instantiate the list and populate it with objekt from the database.
            employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(RepositoryHelper.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT FirstName, LastName, CPRNum, PhoneNum, Email, Address, DepartmentName FROM EMPLOYEE", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        (
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["CPRNum"].ToString(),
                            reader["PhoneNum"].ToString(),
                            reader["Email"].ToString(),
                            reader["Address"].ToString(),
                            // Here we use the departmentrepo to get a department objekt and assign it to the employee instead of just the name of the department.
                            departmentRepo.GetDepartment(reader["DepartmentName"].ToString())
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
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE(FirstName, LastName, CPRNum, PhoneNum, Email, Address, DepartmentName)"+ "VALUES(@FirstName, @LastName, @CPRNum, @PhoneNum, @Email, @Address, @DepartmentName)", connection);
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = employee.LastName;
                cmd.Parameters.Add("@CPRNum", SqlDbType.NVarChar).Value = employee.CPRNum;
                cmd.Parameters.Add("@PhoneNum", SqlDbType.NVarChar).Value = employee.PhoneNum;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = employee.Address;
                cmd.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = employee.Department.Name;
                cmd.ExecuteNonQuery();
                employees.Add(employee);

            }
        }
        public List<Employee> GetEmployees() 
        {
            // Returns all employees.
            return employees;
        }
    }
}
