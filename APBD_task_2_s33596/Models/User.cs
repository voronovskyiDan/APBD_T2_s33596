using APBD_T2_s33596.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Models
{
    public class User
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public UserRole Role { get; private set; }

        public List<Rental> Rentals { get; private set; } = new List<Rental>();

        public User(string name, string surname, string email, UserRole role) 
        {
            Id = _idCounter++;
            Name = name;
            Surname = surname;
            Role = role;
        }

        public Rental rentEquipment(Equipment equipment, int days)
        {
            int count = Rentals.Count(r => r.Returned == null);
            switch (Role)
            {
                case UserRole.Student:
                    if (count >= 2)
                        throw new InvalidOperationException("Student cannot have more than 2 active rentals");
                    break;
                case UserRole.Employee:
                    if(count >= 5)
                        throw new InvalidOperationException("Employee cannot have more than 5 active rentals");
                    break;
                default:
                    throw new InvalidOperationException("Unknown user role");
            }

            if (equipment.Status == EquipmentStatus.Unavailable)
                throw new Exception("Equipment is not available");
            DateTime from = DateTime.Now;
            DateTime dueTo = from.AddDays(days);
            Rental rental = new Rental(this, equipment, from, dueTo);
            Rentals.Add(rental);
            equipment.Status = EquipmentStatus.Rented;

            return rental;
        }
    }

}
