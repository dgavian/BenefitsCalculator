using Api.Models;
using System.Text.Json;

namespace Api.Repositories
{
    public abstract class Repository<T> where T : class?
    {
        protected async Task<List<Employee>> GetAllEmployeesAsync()
        {
            string fileName = @"Data\employees.json";
            using FileStream openStream = File.OpenRead(fileName);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Employee>? employees =
                await JsonSerializer.DeserializeAsync<List<Employee>>(openStream, options);
            return employees ?? new List<Employee>();
        }

        // This approach won't work well if anything beyond basic CRUD operations are required.
        public abstract Task<List<T>> GetAllAsync();

        public abstract Task<T?> GetAsync(int id);
    }
}
