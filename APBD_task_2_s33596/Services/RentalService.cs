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
        private readonly SingletonRepository repository;
        public RentalService()
        {
            repository = SingletonRepository.Instance;
        }
        public List<Rental> GetActiveRentals(int userId)
        {
            return repository.Rentals.Where(r => r.UserId == userId && r.IsActive()).ToList();
        }
        public List<Rental> GetOverdueRentals()
        {
            return repository.Rentals.Where(r => r.IsOverdue()).ToList();
        }
        public async Task<Rental> RentEquipmentAsync(int userId, int equipmentId, int days)
        {
            var user = repository.Users.FirstOrDefault(u => u.Id == userId);
            var equipment = repository.Equipments.FirstOrDefault(e => e.Id == equipmentId);

            if (equipment == null)
                throw new Exception("Equipment not found");
            if (user == null)
                throw new Exception("User not found");

            var activeRentals = repository.Rentals
                .Where(r => r.UserId == userId && r.IsActive()).ToList();

            var rental = Rental.Create(user, equipment, activeRentals, DateTime.Now, days);

            repository.Rentals.Add(rental);
            equipment.MarkAsRented();
            await repository.SaveAsync();

            return rental;
        }
        public async Task<decimal> ReturnEquipmentAsync(int rentalId)
        {
            Rental rental = repository.Rentals.FirstOrDefault(r => r.Id == rentalId);
            Equipment equipment = repository.Equipments.FirstOrDefault(e => e.Id == rental.EquipmentId);

            if (rental == null)
                throw new Exception("Rental not found");
            if (equipment == null)
                throw new Exception("Equipment not found");

            decimal res = rental.ReturnEquipment();
            equipment.MarkAsAvailable();
            await repository.SaveAsync();

            return res;
        }
        public string GenerateReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("Rental Report:");
            foreach (var user in repository.Users)
            {
                report.AppendLine($"User: {user.Name} (ID: {user.Id})");

                var rentals = repository.Rentals.Where(r => r.UserId == user.Id).ToList();
                foreach (var rental in rentals)
                {
                    var equipment = repository.Equipments.First(e => e.Id == rental.EquipmentId);
                    string status = rental.Returned == null ? "Active" : "Returned";
                    report.AppendLine($"  - Equipment: {equipment.Name} (ID: {rental.EquipmentId}), From: {rental.From}, Due To: {rental.DueTo}, Status: {status}");
                }
                report.AppendLine();
            }
            return report.ToString();
        }

    }
}