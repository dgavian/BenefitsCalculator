using Api.Models;
using System;
using Xunit;

namespace ApiTests.UnitTests
{
    public class PaycheckTests
    {
        private const int DefaultEmployeeId = 42;
        private const int RootDependentId = 3;

        private const decimal DefaultSalary = 78000m;

        private static readonly DateTime _defaultEmployeeBirthday = new DateTime(1989, 2, 16);
        private static readonly DateTime _defaultPartnerBirthday = new DateTime(1991, 7, 10);
        private static readonly DateTime _rootChildBirthday = new DateTime(2019, 11, 2);

        [Fact]
        public void Calculate_NoDependents_PopulatesExpectedData()
        {
            var employee = MakeEmployee(DefaultEmployeeId, _defaultEmployeeBirthday, DefaultSalary);
            var expectedGrossPay = 3000m;
            var expectedDeductions = 461.5384615384615m;
            var expectedEmployeeName = "Test Employee";
            var paycheck = MakeSut();

            paycheck.Calculate(employee);

            Assert.Equal(expectedGrossPay, paycheck.GrossPay);
            Assert.Equal(expectedDeductions, paycheck.Deductions);
            Assert.Equal(expectedEmployeeName, paycheck.EmployeeName);
            Assert.Equal(DefaultEmployeeId, paycheck.EmployeeId);
        }

        [Fact]
        public void Calculate_MultipleDependents_PopulatesExpectedData()
        {
            var numChildren = 6;
            var employee = MakeEmployee(DefaultEmployeeId, _defaultEmployeeBirthday, DefaultSalary);
            MakePartner(employee, Relationship.DomesticPartner);
            MakeChildren(numChildren, employee);
            var expectedGrossPay = 3000m;
            // TODO: other expected values here:
        }

        private static Paycheck MakeSut()
        {
            return new Paycheck();
        }

        private static Employee MakeEmployee(int id, DateTime dateOfBirth, decimal salary)
        {
            return new Employee
            {
                Id = id,
                DateOfBirth = dateOfBirth,
                Salary = salary,
                FirstName = "Test",
                LastName = "Employee"
            };
        }

        private static void MakePartner(Employee employee, Relationship partnerType)
        {
            var partner = MakeDependent(1, partnerType, _defaultPartnerBirthday);
            partner.Employee = employee;
            partner.EmployeeId = employee.Id;
        }

        private static void MakeChildren(int numChildren, Employee employee)
        {
            for (int i = 0; i < numChildren; i++)
            {
                var birthday = _rootChildBirthday.AddYears(i);
                var child = MakeDependent(i + RootDependentId, Relationship.Child, birthday);
                child.Employee = employee;
                child.EmployeeId = employee.Id;
                employee.Children.Add((Child)child);
            }
        }

        private static Dependent MakeDependent(int id, Relationship relationship, DateTime birthday)
        {
            var result = Dependent.Create(relationship);

            result.Id = id;
            result.DateOfBirth = birthday;
            result.FirstName = $"Dependent{id}";
            result.LastName = "Test" ;

            return result;
        }
    }
}
