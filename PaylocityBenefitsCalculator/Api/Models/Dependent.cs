namespace Api.Models;

public abstract class Dependent
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public static Dependent Create(Relationship relationship) =>

    relationship switch
    {
        Relationship.Child => new Child(),
        Relationship.Spouse => new Partner(Relationship.Spouse),
        Relationship.DomesticPartner => new Partner(Relationship.DomesticPartner),
        _ => throw new NotSupportedException()
    };
}
