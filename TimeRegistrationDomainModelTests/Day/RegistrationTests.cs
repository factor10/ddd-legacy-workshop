using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class When_creating_a_registration
    {
        private Registration registration;
        
        [SetUp]
        public void SetUp()
        {
            var project = new Project(new Customer("Ikea"), "X");
            registration = new Registration(Duration.Create("3:00"), "Programming", project);
        }

        [Test]
        public void Then_the_hours_is_set()
        {
            registration.Duration.Hours.Should().Be(3);
        }

        [Test]
        public void Then_the_activity_is_set()
        {
            registration.Activity.Should().Be("Programming");
        }

        [Test]
        public void Then_the_project_is_set()
        {
            registration.ProjectSnapshot.Name.Should().Be("X");
        }
    }
}
