using APBD_T2_s33596.Application.Interfaces;
using APBD_T2_s33596.Domain.Models;
using APBD_T2_s33596.Infrastucture.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Application.Services
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
