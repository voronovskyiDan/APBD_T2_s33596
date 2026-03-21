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
        public async Task AddEquipmentAsync(Equipment equipment)
        {
            repository.Equipments.Add(equipment);
            await repository.SaveAsync();
        }
        public async Task AddUserAsync(User user)
        {
            repository.Users.Add(user);
            await repository.SaveAsync();
        }
        public List<Equipment> GetAllAvaliableEquipment()
        {
            return repository.Equipments.Where(e => e.IsAvailable()).ToList();
        }
        public List<Equipment> GetAllEquipment()
        {
            return repository.Equipments;
        }
        public List<Rental> GetActiveRentals(int userId)
        {
            return repository.Rentals.Where(r => r.User.Id == userId && r.IsActive()).ToList();
        }
        public List<Rental> GetOverdueRentals()
        {
            return repository.Rentals.Where(r => r.IsOverdue()).ToList();
        }
        public async Task<Rental> RentEquipmentAsync(int userId, int equipmentId, int days)
        {
            var user = repository.Users.First(u => u.Id == userId);
            var equipment = repository.Equipments.First(e => e.Id == equipmentId);

            if (!equipment.IsAvailable())
                throw new InvalidOperationException("Equipment is not available.");

            int activeRentals = repository.Rentals
                .Count(r => r.User.Id == userId && r.IsActive());

            if (activeRentals >= user.GetRentalLimit())
                throw new InvalidOperationException("User exceeded rental limit.");

            var rental = new Rental(user, equipment, DateTime.Now, DateTime.Now.AddDays(days));

            repository.Rentals.Add(rental);
            equipment.MarkAsRented();
            await repository.SaveAsync();

            return rental;
        }
        public string GenerateReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("Rental Report:");
            foreach (var user in repository.Users)
            {
                report.AppendLine($"User: {user.Name} (ID: {user.Id})");

                var rentals = repository.Rentals.Where(r => r.User.Id == user.Id).ToList();
                foreach (var rental in rentals)
                {
                    string status = rental.Returned == null ? "Active" : "Returned";
                    report.AppendLine($"  - Equipment: {rental.Equipment.Name} (ID: {rental.Equipment.Id}), From: {rental.From}, Due To: {rental.DueTo}, Status: {status}");
                }
                report.AppendLine();
            }
            return report.ToString();
        }
        public async Task<double> ReturnEquipmentAsync(int rentalId)
        {
            Rental rental = repository.Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null)
                throw new Exception("Rental not found");
            double res = rental.ReturnEquipment();
            await repository.SaveAsync();
            return res;
        }
        public async Task MarkAsUnavailableAsync(int equipmentId)
        {
            Equipment equipment = repository.Equipments.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null)
                throw new Exception("Equipment not found");
            equipment.MarkAsUnavailable();
            await repository.SaveAsync();
        }
    }
}