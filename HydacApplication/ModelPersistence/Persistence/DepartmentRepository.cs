using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using ModelPersistence.Model;



namespace ModelPersistence.Persistence
{
    public class DepartmentRepository : IRepository<Department>
    {
        private List<Department> departments;

        public DepartmentRepository()
        {
            // When DepartmentRepository is instantiated, the Department list becomes instantiated and populated with department objects from the database.
            departments = new List<Department>();
            //using (SqlConnection con = new SqlConnection(RepositoryHelper.connectionstring))
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT * FROM DEPARTMENT", con);
            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            Department department = new Department(reader["Name"].ToString());
            //            departments.Add(department);
            //        }
            //    }
            //}
        }
         
        public void Add (Department department)
        {
            // Adds a Department objekt to the list and to the database.
            using (SqlConnection connection = new SqlConnection(RepositoryHelper.connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO DEPARTMENT(Name)" + "VALUES(@Name)", connection);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = department.Name;
                cmd.ExecuteNonQuery();
                departments.Add(department);

            }
        }
        public Department GetDepartment(string name)
        {
            // Returns a Department with the given name.
            return departments.Find(department => department.Name == name);
        }
        public List<Department> GetDepartments()
        {
            // Returns the Department list.
            return departments;
        }
    }
}
