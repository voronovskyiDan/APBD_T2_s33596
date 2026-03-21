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

        public User(string name, string surname, string email, UserRole role) 
        {
            Id = _idCounter++;
            Name = name;
            Surname = surname;
            Role = role;
        }
        public int GetRentalLimit()
        {
            return Role switch
            {
                UserRole.Student => 2,
                UserRole.Employee => 5,
                _ => throw new InvalidOperationException("Unknown role")
            };
        }
    }

}
