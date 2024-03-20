using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydac_Login_Station.Model.Domain;
using Hydac_Login_Station.Model.Persistance;

namespace Hydac_Login_Station.ViewModel
{
    public class MainViewModel
    {
        private EmployeeRepository employeeRepo = new EmployeeRepository();
        private ShiftRepository shiftRepo = new ShiftRepository();
        public MainViewModel() { }
        public string CheckIn(long id)
        {
            Employee emp = employeeRepo.GetEmployee(id);
            if (emp != null)
            {
                string result = shiftRepo.EditShift(emp.EmployeeId);
                return $"{emp.FirstName} {emp.LastName} er {result}";
            }
            else
            {
                return "Du ikke i systemet";
            }
        }
    }
}
