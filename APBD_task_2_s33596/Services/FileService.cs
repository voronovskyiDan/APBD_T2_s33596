using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using APBD_T2_s33596.Interfaces;

namespace APBD_T2_s33596.Services
{
    internal class JsonFileService : IJsonFileService
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonFileService(JsonSerializerOptions? jsonOptions = null)
        {
            _jsonOptions = jsonOptions ?? new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task SaveAsync<T>(T data, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(data, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<T?> LoadAsync<T>(string filePath, bool createIfAbsent = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                if (!createIfAbsent)
                    throw new FileNotFoundException("File not found.", filePath);

                await File.WriteAllTextAsync(filePath, "null");
                return default;
            }

            string json = await File.ReadAllTextAsync(filePath);

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
    }
}
