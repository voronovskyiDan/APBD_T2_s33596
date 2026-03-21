using APBD_T2_s33596.Controller;
using APBD_T2_s33596.Database;
using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using APBD_T2_s33596.Services;

class Program
{
    static void Main(string[] args)
    {
        var db = SingletonRepository.Instance;

        IRentalService rentalService = new RentalService();
        RentalContoller rentalContoller = new RentalContoller(rentalService);



    }
}