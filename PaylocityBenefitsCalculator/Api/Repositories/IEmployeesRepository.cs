using Api.Dtos.Employee;
using Api.Models;

namespace Api.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeAsync(int id);
    }
}