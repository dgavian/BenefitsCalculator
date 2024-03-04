using Api.Models;
using System.Text.Json;

namespace Api.Repositories
{
    public abstract class Repository<T> where T : class?
    {
        // Simulate a database here;
        // In a production application we'd obviously want to use
        // a real database like postgresql or mongodb.
        protected async Task<List<Employee>> GetAllEmployeesAsync()
        {
            // Use Path.Combine instead of hard-coding the separator
            // to allow this to work in multiple environments.
            string fileName = Path.Combine("Data", "employees.json");
            using FileStream openStream = File.OpenRead(fileName);
            // Normally I'd opt for the Newtonsoft library for this type of thing,
            // but since this data storage is a temporary solution,
            // just went with the built-in stuff.
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Employee>? employees =
                await JsonSerializer.DeserializeAsync<List<Employee>>(openStream, options);
            return employees ?? new List<Employee>();
        }

        public abstract Task<List<T>> GetAllAsync();

        public abstract Task<T?> GetAsync(int id);
    }
}
