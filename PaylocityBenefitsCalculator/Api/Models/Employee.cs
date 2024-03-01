namespace Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Partner? Partner { get; set; }
    public ICollection<Child> Children { get; set; } = new List<Child>();
}
