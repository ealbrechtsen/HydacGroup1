using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydac_Login_Station.Model.Domain;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;

namespace Hydac_Login_Station.Model.Persistance
{
    public class EmployeeRepository
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static string? connectionString = configuration.GetConnectionString("MyDBconnection");
        public EmployeeRepository() { }
        public Employee GetEmployee(long id)
        {
            Employee emp = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeId, FirstName, LastName, KeyChipId FROM EMPLOYEE WHERE KeyChipId = @KeyChipId AND EmploymentStatus = 1", con);
                cmd.Parameters.Add("@KeyChipId", SqlDbType.BigInt).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emp = new Employee
                        (
                            int.Parse(reader["EmployeeId"].ToString()),
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            long.Parse(reader["KeyChipId"].ToString())
                        );
                    }
                }
            }
            return emp;
        }
    }
}
