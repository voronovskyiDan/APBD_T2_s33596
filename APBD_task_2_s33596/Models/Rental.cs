using APBD_T2_s33596.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APBD_T2_s33596.Models
{
    public class Rental
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int EquipmentId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime DueTo { get; private set; }
        public DateTime? Returned { get; private set; }
        public decimal Penalty { get; private set; }


        [JsonConstructor]
        public Rental(int id, int userId, int equipmentId, DateTime from, DateTime dueTo, DateTime? returned, decimal penalty)
        {
            Id = id;
            UserId = userId;
            EquipmentId = equipmentId;
            From = from;
            DueTo = dueTo;
            Returned = returned;
            Penalty = penalty;
        }

        //I know this constructor should be private but I have public constructor for Deserialization purposes and
        //If I want this to make private I need to add another Entities for Serialization/Deserialization and map it to Domain Entities
        //which is overkill for this project, so I will just leave it public and add comment that it should be private in real world application
        public Rental(User user, Equipment equipment, DateTime from, DateTime dueTo) 
        {
            Id = _idCounter++;
            UserId = user.Id;
            EquipmentId = equipment.Id;
            From = from;
            DueTo = dueTo;
            Penalty = 0;
        }
        public static Rental Create(
            User user,
            Equipment equipment,
            IReadOnlyCollection<Rental> activeRentals,
            DateTime from,
            int days)
        {

            if (days <= 0)
                throw new InvalidOperationException("Rental days must be greater than zero.");

            if (!equipment.IsAvailable())
                throw new InvalidOperationException("Equipment is not available.");

            if (activeRentals.Count >= user.GetRentalLimit())
                throw new InvalidOperationException("User exceeded rental limit.");

            return new Rental(user, equipment, from, from.AddDays(days));
        }

        public bool IsActive()
        {
            return Returned == null;
        }
        public bool IsOverdue()
        {
            return Returned == null && DateTime.Now > DueTo;
        }
        public decimal ReturnEquipment()
        {
            if (Returned != null)
                throw new InvalidOperationException("Equipment already returned");
            Returned = DateTime.Now;
            if (Returned > DueTo)
            {
                TimeSpan overdueTime = Returned.Value - DueTo;
                Penalty = (decimal)(overdueTime.TotalDays * 3.45); 
            }
            return Penalty;
        }
        public static void setIdCounter(int count)
        {
            _idCounter = count;
        }
    }
}
