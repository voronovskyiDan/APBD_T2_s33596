using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public class Laptop : Equipment
    {
        public int RamGb { get; set; }
        public int SceenSize { get; set; }

        public Laptop(string name, int ramGb, int sceenSize)
        : base(name)
        {
            RamGb = ramGb;
            SceenSize = sceenSize;
        }
    }
}
