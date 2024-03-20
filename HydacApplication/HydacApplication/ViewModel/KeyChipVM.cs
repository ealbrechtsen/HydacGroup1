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
        public long KeyChipId { get; set; }
        public bool InUse {  get; set; }
        public KeyChipVM(KeyChip keyChip)
        {
            KeyChipId = keyChip.KeyChipId;
            InUse = keyChip.InUse;
        }
        public KeyChip GetKeyChip(KeyChipRepository repo) // uses the repository to get the original non vm object, based on the Id of the current Vm object.
        {
            return repo.GetKeyChip(KeyChipId);
        }
    }
}
