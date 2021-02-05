using System;
using System.Linq;
using factor10.TimeRegistration.DomainModel;
using factor10.TimeRegistration.Repositories;
using NUnit.Framework;
using FluentAssertions;

namespace TimeRegistrationRepositoriesTests
{
    public abstract class When_saving_days
    {
        private readonly IDays days;
        private readonly Guid id = Guid.NewGuid();

        protected When_saving_days(IDays days)
        {
            this.days = days;

            var consultant = new Consultant(id, "Jenny", "Jansson");

            var firstDay = new Day(consultant, DateTime.Today);
            firstDay.AddRegistration(new Registration(new Duration(60), "Programming", new Project(new Customer("Volvo"), "New app")));
            days.Save(firstDay);
            
            var secondDay = new Day(consultant, DateTime.Today.AddDays(4));
            days.Save(secondDay);
        }

        [Test]
        public void Then_the_first_day_can_be_reconstituted()
        {
            var day = days.CertainDayForConsultant(new Consultant(id, "Jenny", "Jansson"), DateTime.Today);

            var expected = new {Date = DateTime.Today, Consultant = new {Person = new {FirstName = "Jenny", LastName = "Jansson"}}};
            day.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Then_the_second_day_can_be_reconstituted()
        {
            var day = days.CertainDayForConsultant(new Consultant(id, "Jenny", "Jansson"), DateTime.Today.AddDays(4));

            var expected = new {Date = DateTime.Today.AddDays(4), Consultant = new {Person = new {FirstName = "Jenny", LastName = "Jansson"}}};
            day.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Then_the_first_day_can_be_updated_and_reconstitued()
        {
            var day = days.CertainDayForConsultant(new Consultant(id, "Jenny", "Jansson"), DateTime.Today);
            day.AddRegistration(new Registration(new Duration(120), "Programming", new Project(new Customer("Volvo"), "New app")));
            days.Save(day);

            var updatedDay = days.CertainDayForConsultant(new Consultant(id, "Jenny", "Jansson"), DateTime.Today);
            updatedDay.Hours.Should().Be(3);
        }
        
        
        [Test]
        public void Then_day_can_not_be_found_for_non_existing_consultant()
        {
            var day = days.CertainDayForConsultant(new Consultant(Guid.NewGuid(), "Not Existing"), DateTime.Today);
            day.Should().BeNull();
        }
        [Test]

        public void Then_day_can_not_be_found_for_non_existing_date()
        {
            var day = days.CertainDayForConsultant(new Consultant(id, "Jenny", "Jansson"), DateTime.Today.AddDays(-1));
            day.Should().BeNull();
        }

        [Test]
        public void Then_the_first_day_can_be_found_in_the_interval()
        {
            var resultDates = days.Between(DateTime.Today.AddDays(-2), DateTime.Today.AddDays(3)).Select(d => d.Date);
            resultDates.Should().Equal(DateTime.Today);
        }
    }

    [TestFixture]
    public class When_saving_days_Fake : When_saving_days
    {
        public When_saving_days_Fake() : base(new DaysFake()) {}
    } 

    [TestFixture]
    public class When_saving_days_LiteDB : When_saving_days
    {
        public When_saving_days_LiteDB() : base(new Days(RepositoryTestUtils.CreateNewDatabase())) {}
    } 
}
