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

        public IConfigurationRoot configuration;
        private string? connectionstring;

        public EmployeeRepository()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionstring = configuration.GetConnectionString("MyDBconnection");
            employees = new List<Employee>();
        }
        public void Add(Employee employee)
        {
            using(SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEE(FirstName, LastName, CPRNum, PhoneNum, Email, Address)"+ "VALUES(@FirstName, @LastNameCPRNum, @PhoneNum, @Email, @Address)", connection);
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.FirstName;
                cmd.Parameters.Add("@LastName,", SqlDbType.NVarChar).Value = employee.LastName;
                cmd.Parameters.Add("@CPRNum,", SqlDbType.NVarChar).Value = employee.CPRNum;
                cmd.Parameters.Add("@PhoneNum,", SqlDbType.NVarChar).Value = employee.PhoneNum;
                cmd.Parameters.Add("@Email,", SqlDbType.NVarChar).Value = employee.Email;
                cmd.Parameters.Add("@Address,", SqlDbType.NVarChar).Value = employee.Address;
                cmd.ExecuteNonQuery();
                employees.Add(employee);

            }
        }

        // ** constructor **
        //IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //string? ConnectionString = config.GetConnectionString("MyDBConnection");
    }
}
