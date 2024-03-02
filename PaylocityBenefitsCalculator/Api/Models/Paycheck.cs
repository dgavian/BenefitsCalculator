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

        public decimal Calculate(Employee employee)
        {
            throw new NotImplementedException();
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
