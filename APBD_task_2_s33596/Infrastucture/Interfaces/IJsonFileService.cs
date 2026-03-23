using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_T2_s33596.Infrastucture.Interfaces
{
    internal interface IJsonFileService
    {
        Task SaveAsync<T>(T data, string filePath);
        Task<T?> LoadAsync<T>(string filePath, bool createIfAbsent);
    }
}
