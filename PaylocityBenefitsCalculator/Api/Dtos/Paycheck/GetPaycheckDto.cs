namespace Api.Dtos.Paycheck
{
    public class GetPaycheckDto
    {
        // Arbitrarily chose these; in reality paycheck would probably include PeriodStart and PeriodEnd dates
        // as well as YTD data.
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
    }
}
