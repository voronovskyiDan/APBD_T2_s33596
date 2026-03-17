using APBD_T2_s33596.Database;
using APBD_T2_s33596.Models;

class Program
{
    static void Main(string[] args)
    {
        var db = Singleton.Instance;

        var laptop = new Laptop("Dell XPS 13", 16, 13);
        db.Equipments.Add(laptop);
    }
}