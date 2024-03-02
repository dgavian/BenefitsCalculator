namespace Api.Utilities
{
    // Allow mocking DateTime members for unit testing.
    public abstract class TimeProvider
    {
        private static TimeProvider current;

        static TimeProvider()
        {
            current = new DefaultTimeProvider();
        }

        public static TimeProvider Current
        {
            get { return current; }
            set
            {
                current = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public abstract DateTime Today { get; }
        // Can add Now and UtcNow, but not necessary for this project.

        public static void ResetToDefault() 
        {
            current = new DefaultTimeProvider();
        }
    }
}
