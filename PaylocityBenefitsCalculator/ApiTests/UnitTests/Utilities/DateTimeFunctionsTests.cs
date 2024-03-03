using Api.Utilities;
using Moq;
using System;
using Xunit;

namespace ApiTests.UnitTests.Utilities
{
    public class DateTimeFunctionsTests : IDisposable
    {
        public void Dispose() => TimeProvider.ResetToDefault();

        [Theory]
        [InlineData(1989, 3, 1, 35)]
        [InlineData(1989, 3, 2, 35)]
        [InlineData(1989, 3, 3, 34)]
        public void GetAge_ValidInputs_ExpectedResults(int birthYear, int birthMonth, int birthDay, int expected)
        {
            var fakeTimeProvider = new Mock<TimeProvider>();
            var today = new DateTime(2024, 3, 2);
            fakeTimeProvider.Setup(t => t.Today).Returns(today);
            TimeProvider.Current = fakeTimeProvider.Object;
            var birthday = new DateTime(birthYear, birthMonth, birthDay);

            var actual = DateTimeFunctions.GetAge(birthday);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1989, 3, 1, 34)]
        [InlineData(1989, 2, 28, 35)]
        [InlineData(1992, 2, 28, 32)]
        [InlineData(1992, 2, 29, 32)]
        [InlineData(1992, 3, 1, 31)]
        public void GetAge_LeapDay_ExpectedResults(int birthYear, int birthMonth, int birthDay, int expected)
        {
            var fakeTimeProvider = new Mock<TimeProvider>();
            var today = new DateTime(2024, 2, 29);
            fakeTimeProvider.Setup(t => t.Today).Returns(today);
            TimeProvider.Current = fakeTimeProvider.Object;
            var birthday = new DateTime(birthYear, birthMonth, birthDay);

            var actual = DateTimeFunctions.GetAge(birthday);

            Assert.Equal(expected, actual);
        }
    }
}
