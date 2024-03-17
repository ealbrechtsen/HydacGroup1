using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class KeyChip
    {
        public int KeyChipId { get; set; }

        public KeyChip(int keyChipId)
        {
            KeyChipId = keyChipId;
        }
    }
}
