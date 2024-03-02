
namespace Api.Utilities
{
    public sealed class DefaultTimeProvider : TimeProvider
    {
        public override DateTime Today
        {
            get { return DateTime.Today; }
        }
    }
}
