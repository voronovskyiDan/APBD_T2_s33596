using APBD_T2_s33596.Models;
using APBD_T2_s33596.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Controller
{
    internal class RentalContoller
    {
        private readonly IRentalService rentalService;

        public RentalContoller(IRentalService rentalService)
        {
            this.rentalService = rentalService;
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






        private async Task AddEquipmentAsync(Equipment equipment)
        {
            await rentalService.AddEquipmentAsync(equipment);
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
