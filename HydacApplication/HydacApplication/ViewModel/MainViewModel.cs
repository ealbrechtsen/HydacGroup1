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
        // Repositories
        private DepartmentRepository DepartmentRepo;
        private EmployeeRepository EmployeeRepo;

        // VM Lists
        public ObservableCollection<EmployeeVM> employeesVM { get; set; }
        public ObservableCollection<DepartmentVM> departmentsVM { get; set; }

        // Implemented INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        // Selected Objects from the Views
        private EmployeeVM selectedEmployee;
        public EmployeeVM SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); }
        }
        private DepartmentVM selectedDepartment;

        public DepartmentVM SelectedDepartment
        {
            get { return selectedDepartment; }
            set { selectedDepartment = value; OnPropertyChanged("SelectedEmployee"); }
        }

        // Instantiating MainViewModel, the following repos and populating the VM Lists.
        public MainViewModel() 
        {
            DepartmentRepo = new DepartmentRepository();
            EmployeeRepo = new EmployeeRepository(DepartmentRepo);
            departmentsVM = new ObservableCollection<DepartmentVM>(DepartmentRepo.GetDepartments().Select(department => new DepartmentVM(department)));
            employeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Select(employee => new EmployeeVM(employee)));
        }

        // Creates a new employee object, adds it to VM list, repo list and the database.
        public void CreateEmployee(string firstName, string lastName, string cPRNum, string phoneNum, string email, string address, DepartmentVM department)
        {
            Employee emp = new Employee(firstName, lastName, cPRNum, phoneNum, email, address, department.GetDepartment(DepartmentRepo));
            EmployeeRepo.Add(emp); // This method adds it both to repo and database.
            employeesVM.Add(new EmployeeVM(emp));
        }

        // Creates a new department object, adds it to VM list, repo list and the database.
        public void CreateDepartment(string name)
        {
            Department dpm = new Department(name);
            DepartmentRepo.Add(dpm); // This method adds it both to repo and database.
            departmentsVM.Add(new DepartmentVM(dpm));
        }
    }
}
