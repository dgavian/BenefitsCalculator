using AgeCalculator.Extensions;

namespace Api.Utilities
{
    public static class DateTimeFunctions
    {
        public static int GetAge(DateTime birthday)
        {
            var today = TimeProvider.Current.Today;
            var result = birthday.CalculateAge(today);
            return result.Years;
        }
    }
}
