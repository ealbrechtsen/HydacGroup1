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
    public class KeyChipRepository : IRepository<KeyChip>
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
                SqlCommand cmd = new SqlCommand("SELECT KeyChipId FROM KEYCHIP", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        KeyChip? keyChip = new KeyChip(int.Parse(reader["KeyChipId"].ToString()));
                        if (keyChip == null)
                        {
                            Console.WriteLine("No KeyChipId found");
                        }
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
                SqlCommand cmd = new SqlCommand("INSERT INTO KEYCHIP(KeyChipId)" + "VALUES(@KeyChipId)", connection);
                cmd.Parameters.Add("@KeyChipId", SqlDbType.Int).Value = keyChip.KeyChipId;
                cmd.ExecuteNonQuery();
                keyChips.Add(keyChip);
            }
        }
        public KeyChip GetKeyChip(int keyChipId)
        {
            return keyChips.Find(keyChip => keyChip.KeyChipId == keyChipId);
        }
        public List<KeyChip> GetKeyChips()
        {
            // Returns the KeyChip list.
            return keyChips;
        }

    }
}
