using Api.Models;
using System.ComponentModel;
using System.Text.Json;

namespace Api.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            // TODO: Move to config
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

        public async Task<Employee?> GetEmployeeAsync(int id)
        {
            var employees = await GetAllEmployeesAsync();
            var employee = employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public async Task<List<Dependent>> GetAllDependentsAsync()
        {
            var result = new List<Dependent>();
            var employees = await GetAllEmployeesAsync();
            employees.ForEach(e =>
            {
                if (e.Partner != null)
                {
                    result.Add(e.Partner);
                }
                result.AddRange(e.Children);
            });
            return result;
        }

        public async Task<Dependent?> GetDependentAsync(int id)
        {
            var dependents = await GetAllDependentsAsync();
            var dependent = dependents.FirstOrDefault(d => d.Id == id);
            return dependent;
        }
    }
}
