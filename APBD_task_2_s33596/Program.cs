using APBD_T2_s33596.Controller;
using APBD_T2_s33596.Database;
using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using APBD_T2_s33596.Services;

class Program
{
    static async Task Main(string[] args)
    {
        IRentalService rentalService = new RentalService();
        IUserService userService = new UserService();
        IEquipmentService equipmentService = new EquipmentService();

        var controller = new ApplicationContoller(rentalService, equipmentService, userService);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== EQUIPMENT RENTAL SYSTEM =====");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Add Camera");
            Console.WriteLine("3. Add Laptop");
            Console.WriteLine("4. Add Projector");
            Console.WriteLine("5. Rent Equipment");
            Console.WriteLine("6. Return Equipment");
            Console.WriteLine("7. Mark Equipment \"Under Maintance\"");
            Console.WriteLine("8. Show All Equipment");
            Console.WriteLine("9. Show Available Equipment");
            Console.WriteLine("10. Show Active Rentals");
            Console.WriteLine("11. Show Overdue Rentals");
            Console.WriteLine("12. Show Report");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await AddUser(controller);
                        break;

                    case "2":
                        await AddCamera(controller);
                        break;

                    case "3":
                        await AddLaptop(controller);
                        break;

                    case "4":
                        await AddProjector(controller);
                        break;

                    case "5":
                        await RentEquipment(controller);
                        break;

                    case "6":
                        await ReturnEquipment(controller);
                        break;

                    case "7":
                        await MarkEquipmentAsUnderMaintenance(controller);
                        break;

                    case "8":
                        ShowAllEquipment(controller);
                        break;

                    case "9":
                        ShowAvailableEquipment(controller);
                        break;

                    case "10":
                         showActiveRentals(controller);
                        break;

                    case "11":
                        ShowOverdueRentals(controller);
                        break;

                    case "12":
                        Console.WriteLine(controller.getReport());
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static async Task AddUser(ApplicationContoller controller)
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Surname: ");
        string surname = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Role (student/employee): ");
        string role = Console.ReadLine();

        await controller.AddUserAsync(name, surname, email, role);
        Console.WriteLine("User added.");
    }

    private static async Task AddCamera(ApplicationContoller controller)
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("ISO: ");
        string iso = Console.ReadLine();

        Console.Write("Aperture: ");
        string aperture = Console.ReadLine();

        await controller.AddCameraAsync(name, description, iso, aperture);
        Console.WriteLine("Camera added.");
    }

    private static async Task AddLaptop(ApplicationContoller controller)
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("RAM (GB): ");
        int ram = int.Parse(Console.ReadLine());

        Console.Write("Screen size: ");
        int screen = int.Parse(Console.ReadLine());

        await controller.AddLaptopAsync(name, description, ram, screen);
        Console.WriteLine("Laptop added.");
    }

    private static async Task AddProjector(ApplicationContoller controller)
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Lumens: ");
        int lumens = int.Parse(Console.ReadLine());

        Console.Write("Resolution: ");
        string resolution = Console.ReadLine();

        await controller.AddProjectorAsync(name, description, lumens, resolution);
        Console.WriteLine("Projector added.");
    }

    private static async Task RentEquipment(ApplicationContoller controller)
    {
        Console.Write("Equipment ID: ");
        int eqId = int.Parse(Console.ReadLine());

        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine());

        Console.Write("Days: ");
        int days = int.Parse(Console.ReadLine());

        var rental = await controller.RentEquipmentAsync(eqId, userId, days);
        Console.WriteLine($"Rented! Rental ID: {rental.Id}");
    }

    private static async Task ReturnEquipment(ApplicationContoller controller)
    {
        Console.Write("Rental ID: ");
        int rentalId = int.Parse(Console.ReadLine());

        decimal penalty = await controller.ReturnEquipmentAsync(rentalId);
        Console.WriteLine("Equipment returned. Your penalty is: " + penalty);
    }

    private static async Task MarkEquipmentAsUnderMaintenance(ApplicationContoller controller)
    {
        Console.Write("Equipment ID: ");
        int eqId = int.Parse(Console.ReadLine());
        await controller.MarkEquipmentAsUnderMaintenanceAsync(eqId);
        Console.WriteLine("Equipment marked as under maintenance.");
    }

    private static void ShowAllEquipment(ApplicationContoller controller)
    {
        var list = controller.getAllEquipment();

        foreach (var e in list)
        {
            Console.WriteLine($"{e.Id}: {e.Name} - {e.Descrption} | Status : {e.Status}");
        }
    }

    private static void ShowAvailableEquipment(ApplicationContoller controller)
    {
        var list = controller.getAvailableEquipment();

        foreach (var e in list)
        {
            Console.WriteLine($"{e.Id}: {e.Name}");
        }
    }

    private static void ShowOverdueRentals(ApplicationContoller controller)
    {
        var list = controller.getOverdueRentals();
        foreach (var r in list)
        {
            Console.WriteLine($"Rental ID: {r.Id}, User ID: {r.UserId}, Equipment ID: {r.EquipmentId}, Due To: {r.DueTo}");
        }
    }
    private static void showActiveRentals(ApplicationContoller controller)
    {
        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine());

        var list = controller.getActiveRentals(userId);
        foreach (var r in list)
        {
            Console.WriteLine($"Rental ID: {r.Id}, User ID: {r.UserId}, Equipment ID: {r.EquipmentId}, From: {r.From}, Due To: {r.DueTo}");
        }
    }
}