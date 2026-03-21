using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public class Camera : Equipment
    {
        public string ISO { get; private set; }
        public string Aperture { get; private set; }

        public Camera(string name, string description, string iso, string aperture) 
            : base(name, description)
        {
            ISO = iso;
            Aperture = aperture;
        }
    }
}
