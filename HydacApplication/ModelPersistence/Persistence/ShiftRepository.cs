using Microsoft.Data.SqlClient;
using ModelPersistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Persistence
{
    public class ShiftRepository
    {
        private List<Shift> shifts;
        public ShiftRepository() 
        {
            shifts = new List<Shift>();
            using (SqlConnection con = new SqlConnection(RepositoryHelper.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SHIFT", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Shift shift = new Shift
                        (
                            int.Parse(reader["ShiftId"].ToString()),
                            TimeOnly.Parse(reader["CheckInTime"].ToString()),
                            reader["CheckOutTime"] != DBNull.Value ? TimeOnly.Parse(reader["CheckOutTime"].ToString()) : new TimeOnly(0, 0),
                            DateTime.Parse(reader["ShiftDate"].ToString()),
                            int.Parse(reader["EmployeeId"].ToString())
                        ) ;
                        shifts.Add(shift);
                    }
                }
            }
        }
        public List<Shift> GetShifts(int employeeID)
        {
            return shifts.Where(shift => shift.EmployeeId == employeeID).ToList();
        }
    }
}
