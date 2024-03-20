using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelPersistence.Model;
using ModelPersistence.Persistence;

namespace HydacApplication.ViewModel
{
    public class ShiftVM
    {
        public string CheckInTime {  get; set; }
        public string CheckOutTime {  get; set; }
        public DateOnly Date {  get; set; }

        public ShiftVM(Shift shift)
        {
            CheckInTime = shift.CheckInTime.ToString();
            CheckOutTime = shift.CheckOutTime.ToString();
            Date = shift.Date;
        }
    }
}
