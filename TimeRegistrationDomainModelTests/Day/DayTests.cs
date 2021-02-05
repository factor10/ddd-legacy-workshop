using System;
using System.Linq;
using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class When_creating_a_day
    {
        private Day day;
        private Guid id;
        
        [SetUp]
        public void SetUp()
        {
            var customer = new Customer("Volvo");
            var project = new Project(customer, "ABC");
            
            id = Guid.NewGuid();
            var consultant = new Consultant(id, "Pelle", "Svensson");
            day = new Day(consultant, DateTime.Today);

            day.AddRegistration(new Registration(Duration.Create("2:00"), "Advicing", project));
            day.AddRegistration(new Registration(Duration.Create("3:00"), "Debugging", project));
        }

        [Test]
        public void Then_the_date_is_set()
        {
            day.Date.Should().Be(DateTime.Today);
        }

        [Test]
        public void Then_the_consultant_is_set()
        {
            var expected = new {Id = id, Person = new {FirstName = "Pelle", LastName = "Svensson"}};
            day.Consultant.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Then_there_will_be_two_registrations()
        {
            var r1 = new {Activity = "Advicing", Minutes = 120};
            var r2 = new {Activity = "Debugging", Minutes = 180};
            
            day.Registrations.Select(r => new {Activity = r.Activity, Minutes = r.Duration.Minutes}).Should().Equal(r1, r2);
        }

        [Test]
        public void Then_the_total_numbers_of_hours_on_that_day_will_be_5()
        {
            day.Hours.Should().Be(5);
        }

    }
}
