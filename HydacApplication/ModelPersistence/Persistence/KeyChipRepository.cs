using Microsoft.Data.SqlClient;
using ModelPersistence.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Persistence
{
    public class KeyChipRepository
    {
        private List<KeyChip> keyChips;
        public KeyChipRepository()
        {
            // When the EmployeeRepository is instantiated, we take the Department repo and place it inside a field,
            // so we can use the functions in departmentrepo, to assign departments to employees.
            // Next we instantiate the list and populate it with objekt from the database.
            keyChips = new List<KeyChip>();
            using (SqlConnection con = new SqlConnection(RepositoryHelper.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT KeyChipId, InUse FROM KEYCHIP", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        KeyChip keyChip = new KeyChip
                        (
                            long.Parse(reader["KeyChipId"].ToString()),
                            bool.Parse(reader["InUse"].ToString())
                        );
                        keyChips.Add(keyChip);
                    }
                }
            }
        }
        public void Add(KeyChip keyChip)
        {
            // Adds an employee to the database and the list.
            using (SqlConnection connection = new SqlConnection(RepositoryHelper.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO KEYCHIP(KeyChipId, InUse)" + "VALUES(@KeyChipId, @InUse)", connection);
                cmd.Parameters.Add("@KeyChipId", SqlDbType.BigInt).Value = keyChip.KeyChipId;
                cmd.Parameters.Add("@InUse", SqlDbType.Bit).Value = keyChip.InUse;
                cmd.ExecuteNonQuery();
                keyChips.Add(keyChip);
            }
        }
        public KeyChip GetKeyChip(long keyChipId)
        {
            // Returns the keychip where the ID's Match
            return keyChips.Find(keyChip => keyChip.KeyChipId == keyChipId);
        }
        public List<KeyChip> GetKeyChips()
        {
            // Returns the KeyChip list.
            return keyChips;
        }
        public void UpdateStatus(KeyChip keyChip)
        {
            // Flips the status
            keyChip.InUse = keyChip.InUse == true ? false : true;
            // Finds the first object matching with the employeeId. Then sets that employee.EmploymentStatus to the new employees .employmentStatus.
            // Only updates the local value in the list.
            keyChips.FirstOrDefault(kc => kc.InUse == keyChip.InUse).InUse = keyChip.InUse;
            // Next we update the Database
            using (SqlConnection connection = new SqlConnection(RepositoryHelper.connectionString))
            {
                connection.Open();
                // We use UPDATE table SET Column = @value WHERE column = @Value.
                // We want to UPDATE EmploymentStatus to the employee from the method WHERE EmployeeID matches with the employee from the method.
                SqlCommand cmd = new SqlCommand("UPDATE KEYCHIP SET InUse = @InUse WHERE KeyChipId = @KeyChipId", connection);
                // Here we push the local values into the command statement.
                cmd.Parameters.Add("@KeyChipId", SqlDbType.BigInt).Value = keyChip.KeyChipId;
                cmd.Parameters.Add("@InUse", SqlDbType.Bit).Value = keyChip.InUse;
                // Then we execute the command.
                cmd.ExecuteNonQuery();
            }
        }
    }
}
