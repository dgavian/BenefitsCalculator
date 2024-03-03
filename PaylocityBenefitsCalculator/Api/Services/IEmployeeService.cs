using Api.Dtos.Employee;
using Api.Dtos.Paycheck;

namespace Api.Services
{
    public interface IEmployeeService
    {
        Task<List<GetEmployeeDto>> GetAllEmployeesAsync();

        Task<GetEmployeeDto?> GetEmployeeAsync(int employeeId);

        Task<GetPaycheckDto?> GetPaycheckAsync(int employeeId);
    }
}