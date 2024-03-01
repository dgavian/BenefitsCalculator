namespace Api.Models
{
    public sealed class Child : Dependent
    {
        public Child()
        {
            Relationship = Relationship.Child;
        }
    }
}
