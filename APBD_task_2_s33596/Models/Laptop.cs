using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
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
    }
}
