using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class KeyChip
    {
        public long KeyChipId { get; set; }
        public bool InUse { get; set; }

        public KeyChip(long keyChipId)
        {
            KeyChipId = keyChipId;
            InUse = false;
        }
        public KeyChip(long keyChipId, bool inUse) 
        {
            KeyChipId = keyChipId;
            InUse = inUse;
        }
    }
}
