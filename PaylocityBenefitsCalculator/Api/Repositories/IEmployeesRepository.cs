using Api.Models;

namespace Api.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeAsync(int id);
        Task<List<Dependent>> GetAllDependentsAsync();
        Task<Dependent?> GetDependentAsync(int id);
    }
}