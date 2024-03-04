namespace Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }

    // I elected to split out Partner and Child Dependent subclasses,
    // mainly to represent that an employee can have at most one Partner
    // (if using a "real" database, this constraint could be enforced there as well).
    public Partner? Partner { get; set; }
    public ICollection<Child> Children { get; set; } = new List<Child>();
}
