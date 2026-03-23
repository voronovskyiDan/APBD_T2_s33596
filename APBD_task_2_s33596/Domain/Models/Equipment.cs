using APBD_T2_s33596.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Domain.Models
{

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Camera), "camera")]
    [JsonDerivedType(typeof(Laptop), "laptop")]
    [JsonDerivedType(typeof(Projector), "projector")]
    public abstract class Equipment
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Descrption { get; private set; }
        public EquipmentStatus Status { get; private set; }
        public DateTime AddedDat { get; private set; }

        public Equipment(string name, string descrption = "")
        {
            Id = _idCounter++;
            Name = name;
            Descrption = descrption;
            Status = EquipmentStatus.Available;
            AddedDat = DateTime.Now;
        }


        [JsonConstructor]
        public Equipment(int id, string name, string descrption, EquipmentStatus status, DateTime addedDat)
        {
            Id = id;
            Name = name;
            Descrption = descrption;
            Status = status;
            AddedDat = addedDat;
        }

        public bool IsAvailable()
        {
            return Status == EquipmentStatus.Available;
        }
        public void MarkAsRented()
        {
            if (Status != EquipmentStatus.Available)
                throw new InvalidOperationException("Equipment is not available for rent");
            Status = EquipmentStatus.Rented;
        }
        public void MarkAsUnavailable()
        {
            Status = EquipmentStatus.Unavailable;
        }
        public void MarkAsAvailable()
        {
            if (Status == EquipmentStatus.Available)
                throw new InvalidOperationException("Equipment is already available");
            Status = EquipmentStatus.Available;
        }

        public static void setIdCounter(int count)
        {
            _idCounter = count;
        }

    }
}
