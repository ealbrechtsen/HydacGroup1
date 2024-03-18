using ModelPersistence.Persistence;
using ModelPersistence.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Numerics;

namespace HydacApplication.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Repositories
        private DepartmentRepository DepartmentRepo;
        private EmployeeRepository EmployeeRepo;
        private KeyChipRepository KeyChipRepo;

        // VM Lists
        public ObservableCollection<EmployeeVM> employeesVM { get; set; }
        public ObservableCollection<EmployeeVM> unemployedEmployeesVM { get; set; } // For inaktive employees
        public ObservableCollection<DepartmentVM> departmentsVM { get; set; }
        public ObservableCollection<KeyChipVM> keyChipsVM { get; set; }

        // Implemented INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        // Selected Objects from the Views
        private EmployeeVM selectedEmployee; // Currently used to change an employees status from the listviews.
        public EmployeeVM SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); }
        }
        private DepartmentVM selectedDepartment; // Used to apply selected employee from Combo Box to a new employee instance.

        public DepartmentVM SelectedDepartment
        {
            get { return selectedDepartment; }
            set { selectedDepartment = value; OnPropertyChanged("SelectedEmployee"); }
        }
        private KeyChipVM selectedKeyChip; // Used to apply selected employee from Combo Box to a new employee instance.

        public KeyChipVM SelectedKeyChip
        {
            get { return selectedKeyChip; }
            set { selectedKeyChip = value; OnPropertyChanged("SelectedKeyChip"); }
        }


        // Instantiating MainViewModel, the following repos and populating the VM Lists.
        public MainViewModel() 
        {
            DepartmentRepo = new DepartmentRepository();
            KeyChipRepo = new KeyChipRepository();
            EmployeeRepo = new EmployeeRepository(DepartmentRepo, KeyChipRepo); // EmployeeRepo needs both department and keychip repo exist first and to instantiate,
                                                                                // because employeeRepo uses functions inside both to associatie the objects to an employee. 
            departmentsVM = new ObservableCollection<DepartmentVM>(DepartmentRepo.GetDepartments().Select(department => new DepartmentVM(department))); //Inside Select query, immidiately casts them into VM versions to populate the VM list with.
            keyChipsVM = new ObservableCollection<KeyChipVM>(KeyChipRepo.GetKeyChips().Select(keyChip => new KeyChipVM(keyChip)));
            employeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Where(employee => employee.EmploymentStatus = true) // uses .Where to only Select and Cast objects that matches the condition.
                                                                                          .Select(employee => new EmployeeVM(employee)));
            unemployedEmployeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Where(employee => employee.EmploymentStatus = false) // this list is for deactivated employees.
                                                                                                    .Select(employee => new EmployeeVM(employee)));
        }

        // Creates a new employee object, adds it to VM list, repo list and the database.
        public void CreateEmployee(string firstName, string lastName, KeyChipVM keyChip, DepartmentVM department)
        {
            Employee emp = new Employee(firstName, lastName, keyChip.GetKeyChip(KeyChipRepo), department.GetDepartment(DepartmentRepo)); //KeyChip and Department VM's have methods for returning their non VM versions.
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
        // Creates a new KeyChip object, adds it to VM list, repo list and the database.
        public void CreateKeyChip(long id)
        {
            KeyChip kc = new KeyChip(id);
            KeyChipRepo.Add(kc);
            keyChipsVM.Add(new KeyChipVM(kc));
        }

        // This method checks both employeeVM lists if the employee in the parameters are a match. If it is found and therefore not null(!=null) Then the if statement runs.
        public void SetStatus(EmployeeVM employeeVM) 
        {
            // If employee was found in employeesVM, We take that employee into a field.
            EmployeeVM emp = employeesVM.FirstOrDefault(emp => emp.EmployeeId == employeeVM.EmployeeId);
            if (emp != null)
            {
                // we remove that employee from the list.
                employeesVM.Remove(emp);
                // we switch its status
                emp.EmploymentStatus = false;
                // then add it to the unemployedList
                unemployedEmployeesVM.Add(emp);
                // and we send the update along to the Repo list and Database.
                EmployeeRepo.UpdateStatus(emp.GetEmployee(EmployeeRepo));
            }
            // This part does the same, just in reverse.
            else
            {
                EmployeeVM uemp = unemployedEmployeesVM.FirstOrDefault(uemp => uemp.EmployeeId == employeeVM.EmployeeId);
                unemployedEmployeesVM.Remove(uemp);
                uemp.EmploymentStatus = true;
                employeesVM.Add(uemp);
                EmployeeRepo.UpdateStatus(uemp.GetEmployee(EmployeeRepo));
            }
        }
    }
}
