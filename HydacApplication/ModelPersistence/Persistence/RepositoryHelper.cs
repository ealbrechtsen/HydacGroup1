using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
namespace ModelPersistence.Persistence
{
    public static class RepositoryHelper
    {
        // These Fields store the neccessary data to create a connection to the database. From here all repositories can call "RepositoryHelper.connectionstring"
        // to get the connection information required.
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static string? connectionString = configuration.GetConnectionString("MyDBconnection");
    }
}
