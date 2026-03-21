using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public class Projector: Equipment
    {
        public int Lumens { get; set; }
        public string AspectRatio { get; set; }

        public Projector(string name, string description, int lumens, string aspectRatio) 
            : base(name, description)
        {
            Lumens = lumens;
            AspectRatio = aspectRatio;
        }
    }
}
