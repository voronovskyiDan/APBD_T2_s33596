using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Controller
{
    internal class RentalContoller
    {
        private readonly IJsonFileService fileService;
        private readonly IRentalService rentalService;

        public RentalContoller(IJsonFileService fileService, IRentalService rentalService)
        {
            this.fileService = fileService;
            this.rentalService = rentalService;
        }

        void AddUser(User user)
        {

        }
        void AddEquipment(Equipment equipment)
        {

        }
    }
}
