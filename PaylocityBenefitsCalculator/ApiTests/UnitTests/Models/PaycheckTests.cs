using Api.Models;
using System;
using Xunit;

namespace ApiTests.UnitTests.Models
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
            // 78000 salary / 26 paychecks per year = 3000
            var expectedGrossPay = 3000m;
            // 1000 per month base cost = 461.5384615384615 per paycheck (1000 * 12 = 12000 per year; 12000 / 26 = 461.5384615384615)
            var expectedDeductions = 461.54m;
            var expectedNetPay = 2538.46m;
            var expectedEmployeeName = "Test Employee";
            var paycheck = MakeSut();

            paycheck.Calculate(employee);

            Assert.Equal(expectedGrossPay, paycheck.GrossPay);
            Assert.Equal(expectedDeductions, paycheck.Deductions);
            Assert.Equal(expectedNetPay, paycheck.NetPay);
            Assert.Equal(expectedEmployeeName, paycheck.EmployeeName);
            Assert.Equal(DefaultEmployeeId, paycheck.EmployeeId);
        }

        [Fact]
        public void Calculate_MultipleDependents_PopulatesExpectedData()
        {
            var numChildren = 6;
            var employee = MakeEmployee(DefaultEmployeeId, _defaultEmployeeBirthday, DefaultSalary);
            MakePartner(employee, Relationship.DomesticPartner, _defaultPartnerBirthday);
            MakeChildren(numChildren, employee);
            var expectedGrossPay = 3000m;
            // 600 per month base dependent deduction = 276.9230769230769 per paycheck
            // (600 * 12 = 7200 per year; 7200 / 26 = 276.9230769230769)
            // Total dependent deductions per paycheck = 1938.461538461538 (276.9230769230769 * 7 = 1938.461538461538
            // (1 partner + 6 children))
            // Total expected deductions = 2400 (461.5384615384615 base employee cost + 1938.461538461538 total dependents cost)
            var expectedDeductions = 2400m;
            var expectedNetPay = 600m;
            var paycheck = MakeSut();

            paycheck.Calculate(employee);

            Assert.Equal(expectedGrossPay, paycheck.GrossPay);
            Assert.Equal(expectedDeductions, paycheck.Deductions);
            Assert.Equal(expectedNetPay, paycheck.NetPay);
        }

        [Fact]
        public void Calculate_OneDependentOverFifty_PopulatesExpectedData()
        {
            var numChildren = 2;
            var employee = MakeEmployee(DefaultEmployeeId, _defaultEmployeeBirthday, DefaultSalary);
            var partnerBirthday = new DateTime(1969, 4, 11);
            MakePartner(employee, Relationship.Spouse, partnerBirthday);
            MakeChildren(numChildren, employee);
            var expectedGrossPay = 3000m;
            // Total base dependent deductions per paycheck = 830.7692307692307
            // (276.9230769230769 * 3 = 830.7692307692307 (1 partner + 2 children))
            // Additional dependent deduction for partner over 50 = 92.30769230769231
            // (200 per month = 2400 per year; 2400 / 26 = 92.30769230769231)
            // Total expected deductions = 1384.615384615385
            // (461.5384615384615 base employee cost + 830.7692307692307 base dependents cost + 92.30769230769231 additional dependent deduction for partner over 50)
            var expectedDeductions = 1384.62m;
            var expectedNetPay = 1615.38m;
            var paycheck = MakeSut();

            paycheck.Calculate(employee);

            Assert.Equal(expectedGrossPay, paycheck.GrossPay);
            Assert.Equal(expectedDeductions, paycheck.Deductions);
            Assert.Equal(expectedNetPay, paycheck.NetPay);
        }

        [Fact]
        public void Calculate_NoDependentsSalaryAboveThreshold_PopulatesExpectedData()
        {
            var salary = 80000.01m;
            var employee = MakeEmployee(DefaultEmployeeId, _defaultEmployeeBirthday, salary);
            // 80000.01 salary / 26 paychecks per year = 3076.923461538462
            var expectedGrossPay = 3076.92m;
            // Requirement: employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
            // Assumption is that this is 2% per year to be split across 26 paychecks, but I'd want to verify that with product.
            // Additional 1600.0002 per year (80000.01 * 0.02) = Additional 61.53846923076923 per paycheck (1600.0002 / 26)
            // 523.0769307692307 (461.5384615384615 base employee cost + 61.53846923076923)
            var expectedDeductions = 523.08m;
            var expectedNetPay = 2553.84m;
            var paycheck = MakeSut();

            paycheck.Calculate(employee);

            Assert.Equal(expectedGrossPay, paycheck.GrossPay);
            Assert.Equal(expectedDeductions, paycheck.Deductions);
            Assert.Equal(expectedNetPay, paycheck.NetPay);
        }

        [Fact]
        public void Calculate_AllEdgeCasesCombined_PopulatesExpectedData()
        {
            // TODO: Update to include salary over 80000.
            var numChildren = 2;
            var employee = MakeEmployee(DefaultEmployeeId, _defaultEmployeeBirthday, DefaultSalary);
            var partnerBirthday = new DateTime(1969, 4, 11);
            MakePartner(employee, Relationship.Spouse, partnerBirthday);
            MakeChildren(numChildren, employee);
            var expectedGrossPay = 3000m;
            // Total base dependent deductions per paycheck = 830.7692307692307
            // (276.9230769230769 * 3 = 830.7692307692307 (1 partner + 2 children))
            // Additional dependent deduction for partner over 50 = 92.30769230769231
            // (200 per month = 2400 per year; 2400 / 26 = 92.30769230769231)
            // Total expected deductions = 1384.615384615385
            // (461.5384615384615 base employee cost + 830.7692307692307 base dependents cost + 92.30769230769231 additional dependent deduction for partner over 50)
            var expectedDeductions = 1384.62m;
            var expectedNetPay = 1615.38m;
            var paycheck = MakeSut();

            paycheck.Calculate(employee);

            Assert.Equal(expectedGrossPay, paycheck.GrossPay);
            Assert.Equal(expectedDeductions, paycheck.Deductions);
            Assert.Equal(expectedNetPay, paycheck.NetPay);
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

        private static void MakePartner(Employee employee, Relationship partnerType, DateTime birthday)
        {
            var partner = MakeDependent(1, partnerType, birthday);
            partner.Employee = employee;
            partner.EmployeeId = employee.Id;
            employee.Partner = (Partner)partner;
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
            result.LastName = "Test";

            return result;
        }
    }
}
