using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class BusinessDay
    {
        public DateOnly Date { get; set; }

        public BusinessDay(DateOnly date)
        {
            Date = date;
        }
    }
}
