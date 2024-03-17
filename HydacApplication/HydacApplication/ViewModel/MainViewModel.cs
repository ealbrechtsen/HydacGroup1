﻿using ModelPersistence.Persistence;
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
        private KeyChipRepository KeyChipRepo;

        // VM Lists
        public ObservableCollection<EmployeeVM> employeesVM { get; set; }
        public ObservableCollection<EmployeeVM> unemployedEmployeesVM { get; set; }
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
        private KeyChipVM selectedKeyChip;

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
            EmployeeRepo = new EmployeeRepository(DepartmentRepo, KeyChipRepo);
            departmentsVM = new ObservableCollection<DepartmentVM>(DepartmentRepo.GetDepartments().Select(department => new DepartmentVM(department)));
            keyChipsVM = new ObservableCollection<KeyChipVM>(KeyChipRepo.GetKeyChips().Select(keyChip => new KeyChipVM(keyChip)));
            employeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Where(employee => employee.EmploymentStatus = true)
                                                                                          .Select(employee => new EmployeeVM(employee)));
            unemployedEmployeesVM = new ObservableCollection<EmployeeVM>(EmployeeRepo.GetEmployees().Where(employee => employee.EmploymentStatus = false)
                                                                                                    .Select(employee => new EmployeeVM(employee)));
        }

        // Creates a new employee object, adds it to VM list, repo list and the database.
        public void CreateEmployee(string firstName, string lastName, KeyChipVM keyChip, DepartmentVM department)
        {
            Employee emp = new Employee(firstName, lastName, keyChip.GetKeyChip(KeyChipRepo), department.GetDepartment(DepartmentRepo));
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
        public void CreateKeyChip()
        {

        }
       
    }
}
