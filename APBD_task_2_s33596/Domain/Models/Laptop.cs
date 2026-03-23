using APBD_T2_s33596.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Domain.Models
{
    public class Laptop : Equipment
    {
        public int RamGb { get; private set; }
        public int SceenSize { get; private set; }

        public Laptop(string name, string description, int ramGb, int sceenSize)
        : base(name, description)
        {
            RamGb = ramGb;
            SceenSize = sceenSize;
        }
        
        [JsonConstructor]
        public Laptop(
            int id,
            string name,
            string descrption,
            EquipmentStatus status,
            DateTime addedDat,
            int ramGb,
            int sceenSize)
            : base(id, name, descrption, status, addedDat)
        {
            RamGb = ramGb;
            SceenSize = sceenSize;
        }
    }
}
