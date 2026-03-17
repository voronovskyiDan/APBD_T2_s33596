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
        void AddUser(User user);
        void AddEquipment(Equipment equipment);

        List<Equipment> GetAllEquipment();
        List<Equipment> GetAllAvaliableEquipment();

        Rental RentEquipment(int userId, int equipmentId, int days);

        string GenerateReport();

    } 
}
