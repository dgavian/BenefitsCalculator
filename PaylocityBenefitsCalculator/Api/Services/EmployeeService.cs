using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
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
            return MapEmployees(employees);
        }

        public async Task<GetEmployeeDto?> GetEmployeeAsync(int employeeId)
        {
            var employee = await _employeesRepo.GetEmployeeAsync(employeeId);
            var result = employee != null ? MapEmployee(employee) : null;
            return result;
        }

        private static List<GetEmployeeDto> MapEmployees(List<Employee> employees)
        {
            var result = new List<GetEmployeeDto>();

            employees.ForEach(e =>
            {
                var mappedEmployee = MapEmployee(e);
                result.Add(mappedEmployee);
            });

            return result;
        }

        private static GetEmployeeDto MapEmployee(Employee employee)
        {
            var result = new GetEmployeeDto
            {
                DateOfBirth = employee.DateOfBirth,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Id = employee.Id,
                Salary = employee.Salary,
                Dependents = MapDependents(employee)
            };

            return result;
        }

        private static List<GetDependentDto> MapDependents(Employee employee)
        {
            var result = new List<GetDependentDto>();

            if (employee.Partner != null)
            {
                var mappedPartner = MapDependent(employee.Partner);
                result.Add(mappedPartner);
            }

            foreach (var child in employee.Children)
            {
                var mappedChild = MapDependent(child);
                result.Add(mappedChild);
            }

            return result;
        }

        private static GetDependentDto MapDependent(Dependent dependent)
        {
            var result = new GetDependentDto
            {
                Id = dependent.Id,
                DateOfBirth = dependent.DateOfBirth,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship
            };

            return result;
        }
    }
}
