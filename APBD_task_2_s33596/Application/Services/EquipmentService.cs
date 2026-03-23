using APBD_T2_s33596.Application.Interfaces;
using APBD_T2_s33596.Domain.Models;
using APBD_T2_s33596.Infrastucture.Database;

namespace APBD_T2_s33596.Application.Services
{

    internal class EquipmentService : IEquipmentService
    {
        private readonly SingletonRepository repository;
        public EquipmentService()
        {
            repository = SingletonRepository.Instance;
        }
        public async Task AddEquipmentAsync(Equipment equipment)
        {
            repository.Equipments.Add(equipment);
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
