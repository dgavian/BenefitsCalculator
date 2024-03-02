using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services
{
    public static class ModelToDtoMapper
    {
        internal static List<GetEmployeeDto> MapEmployees(List<Employee> employees)
        {
            var result = new List<GetEmployeeDto>();

            employees.ForEach(e =>
            {
                var mappedEmployee = MapEmployee(e);
                result.Add(mappedEmployee);
            });

            return result;
        }

        internal static GetEmployeeDto MapEmployee(Employee employee)
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

        internal static List<GetDependentDto> MapDependents(Employee employee)
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

        internal static List<GetDependentDto> MapDependents(List<Dependent> dependents)
        {
            var result = new List<GetDependentDto>();
            dependents.ForEach(dependent =>
            {
                result.Add(MapDependent(dependent));
            });
            return result;
        }

        internal static GetDependentDto MapDependent(Dependent dependent)
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
