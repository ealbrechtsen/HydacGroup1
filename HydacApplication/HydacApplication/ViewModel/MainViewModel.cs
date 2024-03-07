using ModelPersistence.Persistence;
using ModelPersistence.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HydacApplication.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DepartmentRepository DepartmentRepo = new DepartmentRepository();
        private EmployeeRepository EmployeeRepo = new EmployeeRepository();
        public ObservableCollection<EmployeeVM> employeesVM { get; set; } = new ObservableCollection<EmployeeVM>();
        public ObservableCollection<DepartmentVM> departmentVM { get; set; } = new ObservableCollection<DepartmentVM>();
        private EmployeeVM selectedEmployee;
        public EmployeeVM SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel() 
        {

        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
        //public void CreateEmployee(string FirstName, string LastName, string CPRNum, string PhoneNum, string Email, string Address)
        //{
        //    Employee emp = new Employee
        //    (
        //        emp.Firstname = FirstName,
        //        emp.Lastname = LastName,
        //        emp.CPRNum = CPRNum,
        //    )
        //}
    }
}
