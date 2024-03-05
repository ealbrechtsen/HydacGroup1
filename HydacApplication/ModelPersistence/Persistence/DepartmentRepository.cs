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
    public class DepartmentRepository // IRepository
    {
        private List<Department> departments;

        public DepartmentRepository() 
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string? ConnectionString = config.GetConnectionString("MyDBConnection");
        }
         
        public void Add (Department department)
        {
            departments.Add(department);
        }
        //public void Add(Subject subject)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionstring))
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand("INSERT INTO SUBJECT(Name)" + "VALUES(@Name)", connection);
        //        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = subject.Name;
        //        cmd.ExecuteNonQuery();
        //        subjects.Add(subject);
        //    }
        //}
    }
}
