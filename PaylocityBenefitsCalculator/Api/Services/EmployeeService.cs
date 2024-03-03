using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Repository<Employee> _repository;
        private readonly PaycheckCalculator _paycheckCalculator;

        public EmployeeService(Repository<Employee> repository, PaycheckCalculator paycheckCalculator)
        {
            _repository = repository;
            _paycheckCalculator = paycheckCalculator;
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

        public async Task<GetPaycheckDto?> GetPaycheckAsync(int employeeId)
        {
            var employee = await _repository.GetAsync(employeeId);
            if (employee == null)
            {
                return null;
            }
            var calcResult = _paycheckCalculator.CalculatePaycheck(employee);
            var result = ModelToDtoMapper.MapPaycheck(employee, calcResult.GrossPay, calcResult.TotalDeductions, calcResult.NetPay);
            return result;
        }
    }
}
