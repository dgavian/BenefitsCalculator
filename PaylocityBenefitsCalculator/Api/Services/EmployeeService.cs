using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Repositories;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeesRepo;

        public EmployeeService(IEmployeesRepository employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeesRepo.GetAllEmployeesAsync();
            return ModelToDtoMapper.MapEmployees(employees);
        }

        public async Task<GetEmployeeDto?> GetEmployeeAsync(int employeeId)
        {
            var employee = await _employeesRepo.GetEmployeeAsync(employeeId);
            var result = employee != null ? ModelToDtoMapper.MapEmployee(employee) : null;
            return result;
        }

        public async Task<List<GetDependentDto>> GetAllDependentsAsync()
        {
            var dependents = await _employeesRepo.GetAllDependentsAsync();
            return ModelToDtoMapper.MapDependents(dependents);
        }

        public async Task<GetDependentDto?> GetDependentAsync(int dependentId)
        {
            var dependent = await _employeesRepo.GetDependentAsync(dependentId);
            var result = dependent != null ? ModelToDtoMapper.MapDependent(dependent) : null;
            return result;
        }
    }
}
