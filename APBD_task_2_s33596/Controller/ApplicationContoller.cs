using APBD_T2_s33596.Models;
using APBD_T2_s33596.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APBD_T2_s33596.Enum;

namespace APBD_T2_s33596.Controller
{
    internal class ApplicationContoller
    {
        private readonly IRentalService rentalService;
        private readonly IEquipmentService equipmentService;
        private readonly IUserService userService;

        public ApplicationContoller(IRentalService rentalService, IEquipmentService equipmentService, IUserService userService)
        {
            this.rentalService = rentalService;
            this.equipmentService = equipmentService;
            this.userService = userService;
        }

        public async Task AddCameraAsync(string name, string description, string iso, string aperture)
        {
            ValidateBasicData(name, description);

            if (string.IsNullOrWhiteSpace(iso))
                throw new ArgumentException("ISO cannot be empty.");

            if (string.IsNullOrWhiteSpace(aperture))
                throw new ArgumentException("Aperture cannot be empty.");

            Equipment camera = new Camera(name, description, iso, aperture);
            await AddEquipmentAsync(camera);
        }
        public async Task AddLaptopAsync(string name, string description, int ramGb, int screenSize)
        {
            ValidateBasicData(name, description);

            if (ramGb <= 0)
                throw new ArgumentException("RAM must be greater than 0.");

            if (screenSize <= 0)
                throw new ArgumentException("Screen size must be greater than 0.");

            Equipment laptop = new Laptop(name, description, ramGb, screenSize);
            await AddEquipmentAsync(laptop);
        }
        public async Task AddProjectorAsync(string name, string description, int lumens, string resolution)
        {
            ValidateBasicData(name, description);

            if (lumens <= 0)
                throw new ArgumentException("Lumens must be greater than 0.");

            if (string.IsNullOrWhiteSpace(resolution))
                throw new ArgumentException("Resolution cannot be empty.");

            Equipment projector = new Projector(name, description, lumens, resolution);
            await AddEquipmentAsync(projector);
        }
        public async Task AddUserAsync(string name, string surname, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");

            UserRole userRole;
            switch (role.ToLower())
            {
                case "student":
                    userRole = UserRole.Student;
                    break;
                case "employee":
                    userRole = UserRole.Employee;
                    break;
                default:
                    throw new ArgumentException("Invalid role. Must be 'Student' or 'Employee'.");
            }

            User user = new User(name, surname, email, userRole);
            await userService.AddUserAsync(user);
        }
        public async Task<Rental> RentEquipmentAsync(int equipmentId, int userId, int days)
        {
            if(days <= 0)
                throw new ArgumentException("Days must be greater than 0.");
            return await rentalService.RentEquipmentAsync(equipmentId, userId, days);
        }
        public async Task ReturnEquipmentAsync(int rentalId)
        {
            await rentalService.ReturnEquipmentAsync(rentalId);
        }
        public async Task MarkEquipmentAsUnderMaintenanceAsync(int equipmentId)
        {
            await equipmentService.MarkAsUnavailableAsync(equipmentId);
        }

        //Sync because it`s take just from memory, and there is no IO operations
        public List<Equipment> getAllEquipment()
        {
            return equipmentService.GetAllEquipment();
        }
        public List<Equipment> getAvailableEquipment()
        {
            return equipmentService.GetAllAvaliableEquipment();
        }
        public List<Rental> getActiveRentals(int userId)
        {
            return rentalService.GetActiveRentals(userId);
        }
        public List<Rental> getOverdueRentals()
        {
            return rentalService.GetOverdueRentals();
        }
        public string getReport() {
            return rentalService.GenerateReport();
        }

        private async Task AddEquipmentAsync(Equipment equipment)
        {
            await equipmentService.AddEquipmentAsync(equipment);
        }
        private void ValidateBasicData(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");
        }
    }
}
