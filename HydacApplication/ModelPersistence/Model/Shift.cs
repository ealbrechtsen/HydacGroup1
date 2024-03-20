using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class Shift
    {
        public int ShiftId;
        public TimeOnly CheckInTime;
        public TimeOnly? CheckOutTime;
        public DateOnly Date;
        public int EmployeeId;

        public Shift(int shiftId, TimeOnly checkInTime, TimeOnly checkOutTime, DateTime date, int employeeId)
        {
            ShiftId = shiftId;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Date = DateOnly.Parse(date.ToString("yyyy-MM-dd"));
            EmployeeId = employeeId;
        }
    }
}
