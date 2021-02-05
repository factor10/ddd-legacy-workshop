using System;
using System.Linq;
using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class When_creating_a_project
    {
        private Project project;
        private Guid id = Guid.NewGuid();
        
        [SetUp]
        public void SetUp()
        {
            var customer = new Customer("Saab");
            project = new Project(customer, "THE app");

            project.AddActivity("Programming");

            var consultant = new Consultant(id, "Karin", "Andersson");
            project.AddConsultant(consultant);
        }

        [Test]
        public void Then_the_name_is_set()
        {
            project.Name.Should().Be("THE app");
        }

        [Test]
        public void Then_the_customer_is_set()
        {
            project.Customer.Name.Should().Be("Saab");
        }

        [Test]
        public void Then_there_is_one_activity_that_is_Programming()
        {
            project.Activities.Should().Equal("Programming");
        }

        [Test]
        public void Then_there_is_one_consultant_named_Karin_Andersson()
        {
            var expected = new {Id = id, FullName = "Karin Andersson"};
            project.Consultants.Select(c => new {Id = c.Id, FullName = c.Person.FullName}).Should().Equal(expected);
        }

        [Test]
        public void Then_the_project_is_ready_to_get_registrations()
        {
            project.IsReadyToGetTimeRegistrations.Should().BeTrue();
        }

        [Test]
        public void Then_I_can_get_a_snapshot_of_the_project()
        {
            var projectSnapshot = project.TakeSnapshot();
            projectSnapshot.Name.Should().Be(project.Name);
        }
    }
}
