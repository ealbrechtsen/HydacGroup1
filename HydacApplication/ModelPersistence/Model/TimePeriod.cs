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
        public BusinessDay BusinessDay { get; set; }

        public TimePeriod(TimeOnly checkInTime, TimeOnly checkOutTime, BusinessDay businessDay)
        {
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            BusinessDay = businessDay;
        }
    }
}
