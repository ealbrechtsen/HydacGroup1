using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class TimePeriod
    {
        public int TimePeriodId { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly? CheckOutTime { get; set; }
        public DateOnly Date {  get; set; }

        public TimePeriod(TimeOnly checkInTime, TimeOnly checkOutTime, DateOnly date)
        {
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Date = date;
        }
    }
}
