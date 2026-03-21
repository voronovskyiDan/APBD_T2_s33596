using APBD_T2_s33596.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Interfaces
{
    internal interface IRentalService
    {
        Task AddUserAsync(User user);
        Task AddEquipmentAsync(Equipment equipment);

        List<Equipment> GetAllEquipment();
        List<Equipment> GetAllAvaliableEquipment();
        List<Rental> GetActiveRentals(int userId);
        List<Rental> GetOverdueRentals();

        Task<Rental> RentEquipmentAsync(int userId, int equipmentId, int days);
        Task<double> ReturnEquipmentAsync(int rentalId);

        Task MarkAsUnavailableAsync(int equipmentId);

        string GenerateReport();
    } 
}
