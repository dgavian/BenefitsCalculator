namespace Api.Models
{
    public sealed class Partner : Dependent
    {
        public Partner()
        {
             
        }

        public Partner(Relationship partnerType)
        {
            if (partnerType == Relationship.None || partnerType == Relationship.Child)
            {
                throw new ArgumentException(nameof(partnerType));
            }

            Relationship = partnerType;
        }
    }
}
