using APBD_T2_s33596.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Interfaces
{
    internal interface IEquipmentService
    {
        List<Equipment> GetAllEquipment();
        List<Equipment> GetAllAvaliableEquipment();
        Task AddEquipmentAsync(Equipment equipment);
        Task MarkAsUnavailableAsync(int equipmentId);

    }
}
