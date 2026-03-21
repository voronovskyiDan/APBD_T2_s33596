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
        List<Rental> GetOverdueRentals();
        List<Rental> GetActiveRentals(int userId);
        Task<Rental> RentEquipmentAsync(int userId, int equipmentId, int days);
        Task<decimal> ReturnEquipmentAsync(int rentalId);
        string GenerateReport();
    } 
}
