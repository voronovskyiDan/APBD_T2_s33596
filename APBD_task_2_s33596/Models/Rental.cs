using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public class Rental
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public User User { get; private set; }
        public Equipment Equipment { get; private set; }
        public DateTime From { get; private set; }
        public DateTime DueTo { get; private set; }
        public DateTime? Returned { get; private set; }
        public double Penalty { get; private set; }

        public Rental(User user, Equipment equipment, DateTime from, DateTime dueTo)
        {
            Id = _idCounter++;
            User = user;
            Equipment = equipment;
            From = from;
            DueTo = dueTo;
            Penalty = 0;
        }

        public bool IsActive()
        {
            return Returned == null;
        }
        public bool IsOverdue()
        {
            return Returned == null && DateTime.Now > DueTo;
        }
        public double returnEquipment()
        {
            if (Returned != null)
                throw new InvalidOperationException("Equipment already returned");
            Returned = DateTime.Now;
            Equipment.MarkAsAvailable();
            if (Returned > DueTo)
            {
                TimeSpan overdueTime = Returned.Value - DueTo;
                Penalty = Math.Ceiling(overdueTime.TotalDays) * 3.45; 
            }
            return Penalty;
        }
    }
}
