using APBD_T2_s33596.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Interfaces
{
    internal interface IUserService
    {
        Task AddUserAsync(User user);
    }
}
