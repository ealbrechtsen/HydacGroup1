using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;
using ModelPersistence.Model;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ModelPersistence.Persistence
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private List<Employee> employees;

        public List<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }


        public IConfigurationRoot configuration;
        private string? connectionstring;

        public EmployeeRepository()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionstring = configuration.GetConnectionString("MyDBconnection");
            employees = new List<Employee>();
            GetAll();
        }
        public void Add(Employee employee)
        {
            using(SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE(FirstName, LastName, CPRNum, PhoneNum, Email, Address)"+ "VALUES(@FirstName, @LastName, @CPRNum, @PhoneNum, @Email, @Address)", connection);
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = employee.LastName;
                cmd.Parameters.Add("@CPRNum", SqlDbType.NVarChar).Value = employee.CPRNum;
                cmd.Parameters.Add("@PhoneNum", SqlDbType.NVarChar).Value = employee.PhoneNum;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = employee.Address;
                cmd.ExecuteNonQuery();
                employees.Add(employee);
            }
        }
        public List<Employee> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEE", connection);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee(reader["FirstName"].ToString(),
                                                         reader["LastName"].ToString(),
                                                         reader["CPRNum"].ToString(),
                                                         reader["PhoneNum"].ToString(),
                                                         reader["Email"].ToString(),
                                                         reader["Address"].ToString());
                        employees.Add(employee);
                    }
                }
                return employees;
            }
        }

        // ** constructor **
        //IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //string? ConnectionString = config.GetConnectionString("MyDBConnection");
    }
}
