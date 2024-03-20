using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydac_Login_Station.Model;
using Hydac_Login_Station.Model.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Hydac_Login_Station.Model.Persistance
{
    public class ShiftRepository
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static string? connectionString = configuration.GetConnectionString("MyDBconnection");
        public ShiftRepository() { }
        public string EditShift(int id)
        {
            int count;
            string result;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SHIFT WHERE EmployeeId = @EmployeeId AND ShiftDate = @ShiftDate", con);
                cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@ShiftDate", SqlDbType.Date).Value= DateTime.Now.ToString("yyyy-MM-dd");
                count = (int)cmd.ExecuteScalar();
            }
            if (count == 0) 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO SHIFT(CheckInTime, ShiftDate, EmployeeId)" + "VALUES(@CheckInTime, @ShiftDate, @EmployeeId)", con);
                    cmd.Parameters.Add("@CheckInTime", SqlDbType.Time).Value = DateTime.Now.ToString("HH:m");
                    cmd.Parameters.Add("@ShiftDate", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                    result = "Tjekket ind";
                }
            }
            else 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE SHIFT SET CheckOutTime = @CheckOutTime WHERE EmployeeId = @EmployeeId", con);
                    cmd.Parameters.Add("@CheckOutTime", SqlDbType.Time).Value = DateTime.Now.ToString("HH:m");
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
                result = "Tjekket ud";
            }
            return result;
        }
    }
}
