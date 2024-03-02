using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Repository<Employee> _repository;

        public EmployeeService(Repository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();
            return ModelToDtoMapper.MapEmployees(employees);
        }

        public async Task<GetEmployeeDto?> GetEmployeeAsync(int employeeId)
        {
            var employee = await _repository.GetAsync(employeeId);
            var result = employee != null ? ModelToDtoMapper.MapEmployee(employee) : null;
            return result;
        }
    }
}
