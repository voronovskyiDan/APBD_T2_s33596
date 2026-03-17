using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public class Rental
    {
        public User User { get; set; }
        public Equipment Equipment { get; set; }
        public DateTime From { get; set; }
        public DateTime DueTo { get; set; }
        public DateTime? Returned { get; set; }
        public double Penalty { get; set; }

        public Rental(User user, Equipment equipment, DateTime from, DateTime dueTo)
        {
            User = user;
            Equipment = equipment;
            From = from;
            DueTo = dueTo;
            Penalty = 0;
        }
    }
}
