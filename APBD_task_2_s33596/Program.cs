using APBD_T2_s33596.Controller;
using APBD_T2_s33596.Database;
using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using APBD_T2_s33596.Services;

class Program
{
    static void Main(string[] args)
    {
        IRentalService rentalService = new RentalService();
        IUserService userService = new UserService();
        IEquipmentService equipmentService = new EquipmentService();
        ApplicationContoller applicationContoller = new ApplicationContoller(rentalService, equipmentService, userService);

        applicationContoller.AddUserAsync("Doe", "John", "2@gmail.com", "student").Wait();
       // applicationContoller.AddCameraAsync("Canon EOS R5", "High-end mirrorless camera", "100-51200", "f/2.8").Wait();
        //applicationContoller.RentEquipmentAsync(1, 1, 7).Wait();
        
        Console.WriteLine(applicationContoller.getReport());


    }
}