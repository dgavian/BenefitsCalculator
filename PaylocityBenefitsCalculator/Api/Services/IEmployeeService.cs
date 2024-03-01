using Api.Dtos.Dependent;
using Api.Dtos.Employee;

namespace Api.Services
{
    public interface IEmployeeService
    {
        Task<List<GetEmployeeDto>> GetAllEmployeesAsync();

        Task<GetEmployeeDto?> GetEmployeeAsync(int employeeId);

        Task<List<GetDependentDto>> GetAllDependentsAsync();

        Task<GetDependentDto?> GetDependentAsync(int dependentId);
    }
}