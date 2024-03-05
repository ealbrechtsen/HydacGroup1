using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;

namespace ModelPersistence.Persistence
{
    public interface IRepository
    {
        void Add();
    }
}
