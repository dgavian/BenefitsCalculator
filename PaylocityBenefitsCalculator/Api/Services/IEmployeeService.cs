using Api.Dtos.Employee;

namespace Api.Services
{
    public interface IEmployeeService
    {
        Task<List<GetEmployeeDto>> GetAllEmployeesAsync();

        Task<GetEmployeeDto?> GetEmployeeAsync(int employeeId);
    }
}