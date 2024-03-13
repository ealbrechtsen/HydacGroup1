using ModelPersistence.Model;
using ModelPersistence.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydacApplication.ViewModel
{
    public class DepartmentVM
    {
        public string Name { get; set; }
        public DepartmentVM(Department department)
        {
            Name = department.Name;
        }
        public Department GetDepartment(DepartmentRepository repo)
        {
            return repo.GetDepartment(Name);
        }
    }
}
