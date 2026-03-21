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
        private readonly IRentalService rentalService;

        public RentalContoller(IRentalService rentalService)
        {
            this.rentalService = rentalService;
        }


    }
}
