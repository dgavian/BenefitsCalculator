using AgeCalculator.Extensions;

namespace Api.Utilities
{
    public static class DateTimeFunctions
    {
        public static int GetAge(DateTime birthday)
        {
            // Accurately calculating age proved to be pretty messy,
            // so opted for the AgeCalculator NuGet package.
            var today = TimeProvider.Current.Today;
            var result = birthday.CalculateAge(today);
            return result.Years;
        }
    }
}
