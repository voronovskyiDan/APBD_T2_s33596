using APBD_T2_s33596.Database;
using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Services
{
    public class RentalService : IRentalService
    {
        private readonly Singleton database;
        public RentalService()
        {
            database = Singleton.Instance;
        }
        public void AddEquipment(Equipment equipment)
        {
            database.Equipments.Add(equipment);
        }
        public void AddUser(User user)
        {
            database.Users.Add(user);
        }
        public List<Equipment> GetAllAvaliableEquipment()
        {
            return database.Equipments.Where(e => e.IsAvailable()).ToList();
        }
        public List<Equipment> GetAllEquipment()
        {
            return database.Equipments;
        }
        public List<Rental> GetActiveRentals(int userId)
        {
            User user = database.Users.FirstOrDefault(u => u.Id == userId);
            if(user == null)
                throw new Exception("User not found");
            return user.Rentals.Where(r => r.IsActive()).ToList();
        }
        public List<Rental> GetOverdueRentals()
        {
            return database.Rentals.Where(r => r.IsOverdue()).ToList();
        }
        public Rental RentEquipment(int userId, int equipmentId, int days)
        {
            User user = database.Users.FirstOrDefault(u => u.Id == userId);
            Equipment equipment = database.Equipments.FirstOrDefault(e => e.Id == equipmentId);
            if (user == null || equipment == null)
                throw new Exception("Invalid user or equipment");
            return user.rentEquipment(equipment, days);
        }
        public string GenerateReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("Rental Report:");
            foreach (var user in database.Users)
            {
                report.AppendLine($"User: {user.Name} (ID: {user.Id})");
                foreach (var rental in user.Rentals)
                {
                    string status = rental.Returned == null ? "Active" : "Returned";
                    report.AppendLine($"  - Equipment: {rental.Equipment.Name} (ID: {rental.Equipment.Id}), From: {rental.From}, Due To: {rental.DueTo}, Status: {status}");
                }
                report.AppendLine();
            }
            return report.ToString();
        }
        public double ReturnEquipment(int rentalId)
        {
            Rental rental = database.Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null)
                throw new Exception("Rental not found");
            return rental.returnEquipment();
        }
        public void MarkAsUnavailable(int equipmentId)
        {
            Equipment equipment = database.Equipments.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null)
                throw new Exception("Equipment not found");
            equipment.MarkAsUnavailable();
        }
    }
}
