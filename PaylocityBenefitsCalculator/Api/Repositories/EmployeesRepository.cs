using Api.Models;

namespace Api.Repositories
{
    public class EmployeesRepository : Repository<Employee>
    {
        public override async Task<List<Employee>> GetAllAsync()
        {
            return await GetAllEmployeesAsync();
        }

        public override async Task<Employee?> GetAsync(int id)
        {
            var employees = await GetAllAsync();
            var employee = employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }
    }
}
