using APBD_T2_s33596.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public abstract class Equipment
    {
        private static int _idCounter = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public EquipmentStatus Status { get; set; }
        public DateTime AddedDat { get; set; }

        public Equipment( string name, string descrption = "")
        {
            Id = _idCounter++;
            Name = name;
            Descrption = descrption;
            Status = EquipmentStatus.Available;
            AddedDat = DateTime.Now;
        }
    }
}
