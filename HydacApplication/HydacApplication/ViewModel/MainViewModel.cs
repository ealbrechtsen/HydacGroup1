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
        private ShiftRepository ShiftRepo;

        // VM Lists
        public ObservableCollection<EmployeeVM> employeesVM { get; set; }
        public ObservableCollection<EmployeeVM> unemployedEmployeesVM { get; set; } // For inaktive employees
        public ObservableCollection<DepartmentVM> departmentsVM { get; set; }
        public ObservableCollection<KeyChipVM> keyChipsVM { get; set; }
        public ObservableCollection<KeyChipVM> keyChipsInUseVM { get; set; }
        private ObservableCollection<ShiftVM> shiftsVM;

        // Implemented INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<ShiftVM> ShiftsVM 
        {
            get {  return shiftsVM; }
            set 
            {
                shiftsVM = value;
                OnPropertyChanged("ShiftsVM");
            }
        }


        // Selected Objects from the Views
        private EmployeeVM selectedEmployee; // Currently used to change an employees status from the listviews.
        public EmployeeVM SelectedEmployee
        {
            get { return selectedEmployee; }
            set 
            { 
                selectedEmployee = value; 
                OnPropertyChanged("SelectedEmployee");
                if (value != null) 
                {
                    ShiftsVM = new ObservableCollection<ShiftVM>(ShiftRepo.GetShifts(value.EmployeeId).Select(shift => new ShiftVM(shift)));

                }
            }
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
            ShiftRepo = new ShiftRepository();
            EmployeeRepo = new EmployeeRepository(DepartmentRepo, KeyChipRepo); // EmployeeRepo needs both department and keychip repo exist first and to instantiate,
                                                                                // because employeeRepo uses functions inside both to associatie the objects to an employee. 
            departmentsVM = new ObservableCollection<DepartmentVM>(DepartmentRepo.GetDepartments().Select(department => new DepartmentVM(department))); //Inside Select query, immediately casts them into VM versions to populate the VM list with.
            keyChipsVM = new ObservableCollection<KeyChipVM>(KeyChipRepo.GetKeyChips().Where(keyChip => keyChip.InUse == false).Select(keyChip => new KeyChipVM(keyChip)));
            keyChipsInUseVM = new ObservableCollection<KeyChipVM>(KeyChipRepo.GetKeyChips().Where(keyChip => keyChip.InUse == true).Select(keyChip => new KeyChipVM(keyChip)));
            employeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Where(employee => employee.EmploymentStatus == true) // uses .Where to only Select and Cast objects that matches the condition.
                                                                                          .Select(employee => new EmployeeVM(employee)));
            unemployedEmployeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Where(employee => employee.EmploymentStatus == false) // this list is for deactivated employees.
                                                                                                    .Select(employee => new EmployeeVM(employee)));
            ShiftsVM = new ObservableCollection<ShiftVM>();
        }

        // Creates a new employee object, adds it to VM list, repo list and the database.
        public void CreateEmployee(string firstName, string lastName, KeyChipVM keyChip, DepartmentVM department)
        {
            Employee emp = new Employee(firstName, lastName, keyChip.GetKeyChip(KeyChipRepo), department.GetDepartment(DepartmentRepo)); //KeyChip and Department VM's have methods for returning their non VM versions.
            EmployeeRepo.Add(emp); // This method adds it both to repo and database.
            employeesVM.Add(new EmployeeVM(emp));
        }

        // Creates a new department object, adds it to VM list, repo list and the database.
        public string CreateDepartment(string name)
        {
            string result = "Afdeling eksistere";
            if (departmentsVM.FirstOrDefault(department => department.Name == name) == null)
            {
                Department dpm = new Department(name);
                DepartmentRepo.Add(dpm); // This method adds it both to repo and database.
                departmentsVM.Add(new DepartmentVM(dpm));
                result = "Afdeling tilføjet";
            }
            return result;
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
            // If employeeVM was found in employeesVM.
            if (employeesVM.FirstOrDefault(emp => emp.EmployeeId == employeeVM.EmployeeId) != null)
            {
                // we remove that employee from the list.
                employeesVM.Remove(employeeVM);
                // then add it to the unemployedList
                unemployedEmployeesVM.Add(employeeVM);
                // and we send the update along to the Repo list and Database.
                EmployeeRepo.UpdateStatus(employeeVM.GetEmployee(EmployeeRepo));
            }
            // This part does the same, just in reverse.
            else
            {
                unemployedEmployeesVM.Remove(employeeVM);
                employeesVM.Add(employeeVM);
                EmployeeRepo.UpdateStatus(employeeVM.GetEmployee(EmployeeRepo));
            }
        }
        public void SetStatus(KeyChipVM keyChipVM)
        {
            // If employeeVM was found in employeesVM.
            if (keyChipsVM.FirstOrDefault(kc => kc.InUse == keyChipVM.InUse) != null)
            {
                // we remove that employee from the list.
                keyChipsVM.Remove(keyChipVM);
                // then add it to the unemployedList
                keyChipsInUseVM.Add(keyChipVM);
                // and we send the update along to the Repo list and Database.
                KeyChipRepo.UpdateStatus(keyChipVM.GetKeyChip(KeyChipRepo));
            }
            // This part does the same, just in reverse.
            else
            {
                keyChipsInUseVM.Remove(keyChipVM);
                keyChipsVM.Add(keyChipVM);
                KeyChipRepo.UpdateStatus(keyChipVM.GetKeyChip(KeyChipRepo));
            }
        }
    }
}
