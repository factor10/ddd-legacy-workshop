using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class DurationTests
    {
        [Test]
        public void When_creating_a_duration_from_minutes_string()
        {
            var duration = Duration.Create("30 min");
            duration.Minutes.Should().Be(30);
        }
        
        [Test]
        public void When_creating_a_duration_from_decimal_string()
        {
            var duration = Duration.Create("0.5");
            duration.Minutes.Should().Be(30);
        }

        [Test]
        public void When_creating_a_duration_from_colon_string()
        {
            var duration = Duration.Create("0:30");
            duration.Minutes.Should().Be(30);
        }
    }
}