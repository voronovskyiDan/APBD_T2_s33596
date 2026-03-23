using APBD_T2_s33596.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Domain.Models
{
    public class User
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }

        [JsonConstructor]
        public User(int id, string name, string surname, string email, UserRole role)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Role = role;
        }
        public User(string name, string surname, string email, UserRole role) 
        {
            Id = _idCounter++;
            Name = name;
            Surname = surname;
            Email = email;
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
        public static void setIdCounter(int count)
        {
            _idCounter = count;
        }
    }

}
