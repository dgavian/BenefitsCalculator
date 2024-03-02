using Api.Utilities;

namespace Api.Models
{
    public sealed class Paycheck
    {
        public int EmployeeId { get; private set; }
        public string? EmployeeName { get; private set; }
        public decimal GrossPay { get; private set; }
        public decimal Deductions { get; private set; }

        public decimal NetPay
        {
            get
            {
                return GrossPay - Deductions;
            }
        }

        public void CalculateAndFill(Employee employee)
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

            EmployeeId = employee.Id;
            EmployeeName = $"{employee.FirstName} {employee.LastName}";

            var grossPay = CalculateGrossPay(employee.Salary);

            var employeeDeductions = CalculateEmployeeDeductions(employee.Salary);
            var dependentDeductions = CalculateDependentDeductions(employee.Partner, employee.Children);
            var rawTotalDeductions = employeeDeductions + dependentDeductions;

            GrossPay = grossPay;
            Deductions = Math.Round(rawTotalDeductions, 2, MidpointRounding.AwayFromZero);

            static decimal CalculateGrossPay(decimal salary)
            {
                var result = salary / NumberOfPaychecks;
                return Math.Round(result, 2, MidpointRounding.AwayFromZero);
            }

            static decimal CalculateEmployeeDeductions(decimal salary)
            {
                var result = BaseYearlyBenefitsCost / NumberOfPaychecks;
                result += CalculateAdditionalEmployeeDeductions(salary);
                return result;
            }

            static decimal CalculateAdditionalEmployeeDeductions(decimal salary)
            {
                if (salary <= SalaryThreshold)
                {
                    return 0m;
                }
                var yearlyTwoPercentDeduction = salary * AdditionalEmployeeDeductionPercent;
                return yearlyTwoPercentDeduction / NumberOfPaychecks;
            }

            static decimal CalculateDependentDeductions(Dependent? partner, ICollection<Child> children)
            {
                var result = 0m;

                var baseDependentBenefitsCost = BaseYearlyDependentBenefitsCost / NumberOfPaychecks;

                if (partner != null)
                {
                    result += baseDependentBenefitsCost;
                    result += CalculateAdditionalDependentDeductions(partner.DateOfBirth);
                }


                foreach (var dependent in children)
                {
                    result += baseDependentBenefitsCost;
                    result += CalculateAdditionalDependentDeductions(dependent.DateOfBirth);
                }

                return result;
            }

            static decimal CalculateAdditionalDependentDeductions(DateTime dependentBirthday)
            {
                var dependentAge = Helpers.GetAge(dependentBirthday);
                if (dependentAge < AgeThreshold)
                {
                    return 0m;
                }
                return YearlyAdditionalDependentDeductions / NumberOfPaychecks;
            }
        }

        /*public override bool Equals(object? obj)
        {
            var paycheck = obj as Paycheck;
            if (paycheck == null) return false;
            if (ReferenceEquals(this, paycheck)) return true;

            return paycheck.EmployeeId == EmployeeId &&
                paycheck.EmployeeName == EmployeeName &&
                paycheck.GrossPay == GrossPay &&
                paycheck.Deductions == Deductions;
            
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;

                hash = hash * 23 + EmployeeId.GetHashCode();
                hash = hash * 23 + EmployeeName?.GetHashCode() ?? 0;
                hash = hash * 23 + GrossPay.GetHashCode();
                hash = hash * 23 + Deductions.GetHashCode();

                return hash;
            }
        }*/
    }
}
