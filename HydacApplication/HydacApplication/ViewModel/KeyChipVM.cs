using ModelPersistence.Model;
using ModelPersistence.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydacApplication.ViewModel
{
    public class KeyChipVM
    {
        public int KeyChipId { get; set; }
        public KeyChipVM(KeyChip keyChip)
        {
            KeyChipId = keyChip.KeyChipId;
        }
        public KeyChip GetKeyChip(KeyChipRepository repo)
        {
            return repo.GetKeyChip(KeyChipId);
        }

        //public string Name { get; set; }
        //public DepartmentVM(Department department)
        //{
        //    Name = department.Name;
        //}
        //public Department GetDepartment(DepartmentRepository repo)
        //{
        //    return repo.GetDepartment(Name);
        //}
    }
}
