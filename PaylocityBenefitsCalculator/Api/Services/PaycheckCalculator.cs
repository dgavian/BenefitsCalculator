using Api.Models;
using Api.Utilities;

namespace Api.Services
{
    public class PaycheckCalculator
    {
        // May want to have these in configuration.
        const decimal BaseYearlyBenefitsCost = 12000m;
        // Everything that divides by this assumes an exact distribution.
        // In reality, any rounding and/or division discrepancies would need to accounted for
        // (e.g., in final paycheck of year or employment).
        const int NumberOfPaychecks = 26;
        const decimal SalaryThreshold = 80000m;
        const decimal AdditionalEmployeeDeductionPercent = 0.02m;
        const decimal BaseYearlyDependentBenefitsCost = 7200m;
        const int AgeThreshold = 50;
        const decimal YearlyAdditionalDependentDeductions = 2400m;

        private static readonly decimal _baseEmployeeBenefitsCostPerPeriod = BaseYearlyBenefitsCost / NumberOfPaychecks;
        private static readonly decimal _baseDependentBenefitsCostPerPeriod = BaseYearlyDependentBenefitsCost / NumberOfPaychecks;
        private static readonly decimal _additionalDependentDeductionsPerPeriod = YearlyAdditionalDependentDeductions / NumberOfPaychecks;

        public (decimal GrossPay, decimal TotalDeductions, decimal NetPay) CalculatePaycheck(Employee employee)
        {
            // See unit tests for comments on the logic used.
            var grossPay = CalculateGrossPay(employee.Salary);

            var employeeDeductions = CalculateEmployeeDeductions(employee.Salary);
            var dependentDeductions = CalculateDependentDeductions(employee.Partner, employee.Children);
            var rawTotalDeductions = employeeDeductions + dependentDeductions;

            var totalDeductions = Math.Round(rawTotalDeductions, 2, MidpointRounding.AwayFromZero);
            var netPay = grossPay - totalDeductions;

            return (grossPay, totalDeductions, netPay);
        }

        private static decimal CalculateGrossPay(decimal salary)
        {
            var result = salary / NumberOfPaychecks;
            return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }

        private static decimal CalculateEmployeeDeductions(decimal salary)
        {
            var result = _baseEmployeeBenefitsCostPerPeriod;
            result += CalculateAdditionalEmployeeDeductions(salary);
            return result;
        }

        private static decimal CalculateAdditionalEmployeeDeductions(decimal salary)
        {
            if (salary <= SalaryThreshold)
            {
                return 0m;
            }
            var yearlyTwoPercentDeduction = salary * AdditionalEmployeeDeductionPercent;
            return yearlyTwoPercentDeduction / NumberOfPaychecks;
        }

        private static decimal CalculateDependentDeductions(Dependent? partner, ICollection<Child> children)
        {
            var result = 0m;

            if (partner != null)
            {
                result += _baseDependentBenefitsCostPerPeriod;
                result += CalculateAdditionalDependentDeductions(partner.DateOfBirth);
            }


            foreach (var dependent in children)
            {
                result += _baseDependentBenefitsCostPerPeriod;
                result += CalculateAdditionalDependentDeductions(dependent.DateOfBirth);
            }

            return result;
        }

        private static decimal CalculateAdditionalDependentDeductions(DateTime dependentBirthday)
        {
            // This check might only make sense for partners and not children;
            // would want to verify with product.
            var dependentAge = DateTimeFunctions.GetAge(dependentBirthday);
            if (dependentAge < AgeThreshold)
            {
                return 0m;
            }
            return _additionalDependentDeductionsPerPeriod;
        }
    }
}