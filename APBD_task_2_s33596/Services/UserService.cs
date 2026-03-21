using APBD_T2_s33596.Database;
using APBD_T2_s33596.Interfaces;
using APBD_T2_s33596.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Services
{
    internal class UserService : IUserService
    {
        private readonly SingletonRepository repository;
        public UserService()
        {
            repository = SingletonRepository.Instance;
        }
        public async Task AddUserAsync(User user)
        {
            repository.Users.Add(user);
            await repository.SaveAsync();
        }
    }
}
