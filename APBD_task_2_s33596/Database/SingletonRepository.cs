using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using APBD_T2_s33596.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Database
{
    public class SingletonRepository
    {
        private static SingletonRepository? _instance;
        private readonly IJsonFileService _jsonFileService;
        private const string DefaultFilePath = "data/repository.json";

        public static SingletonRepository Instance
        {
            get
            {
                _instance ??= new SingletonRepository();
                return _instance;
            }
        }

        private SingletonRepository() 
        {
            _jsonFileService = new JsonFileService();
            LoadAsync().Wait(); // Load data on initialization
        }

        public List<Equipment> Equipments { get; } = new();
        public List<User> Users { get; } = new();
        public List<Rental> Rentals { get; } = new();

        public async Task SaveAsync(string? filePath = null)
        {
            var data = new RepositoryData
            {
                Equipments = Equipments,
                Users = Users,
                Rentals = Rentals
            };

            await _jsonFileService.SaveAsync(data, filePath ?? DefaultFilePath);
        }

        public async Task LoadAsync(string? filePath = null)
        {
            var data = await _jsonFileService.LoadAsync<RepositoryData>(filePath ?? DefaultFilePath);

            if (data is null)
                return;

            Equipments.Clear();
            Users.Clear();
            Rentals.Clear();

            if (data.Equipments is not null)
                Equipments.AddRange(data.Equipments);

            if (data.Users is not null)
                Users.AddRange(data.Users);

            if (data.Rentals is not null)
                Rentals.AddRange(data.Rentals);
        }


        private class RepositoryData //Not very efficient but it works, I can optimize it later if needed
        {
            public List<Equipment> Equipments { get; set; } = new();
            public List<User> Users { get; set; } = new();
            public List<Rental> Rentals { get; set; } = new();
        }
    }
}