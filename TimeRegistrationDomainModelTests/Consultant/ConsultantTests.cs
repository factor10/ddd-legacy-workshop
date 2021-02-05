using System;
using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class When_creating_a_consultant
    {
        private Consultant consultant;
        private const string idAsString = "b1f585d045634064b5d9418105fd33be";

        [SetUp]
        public void SetUp()
        {
            consultant = new Consultant(new Guid(idAsString), "Jimmy", "Nilsson");
        }

        [Test]
        public void Then_the_values_are_the_expected()
        {
            var expected = new { Id = new Guid(idAsString), Person = new {FirstName = "Jimmy", LastName = "Nilsson"}};
            consultant.Should().BeEquivalentTo(expected);
        }
        
        [TestCase(idAsString, "Jimmy", "Nilsson", ExpectedResult = true)]
        [TestCase("000085d045634064b5d9418105fd0000", "Jimmy", "Nilsson", ExpectedResult = false)]        
        [TestCase(idAsString, "???", "Nilsson", ExpectedResult = false)]        
        [TestCase(idAsString, "Jimmy", "???", ExpectedResult = false)]        
        [TestCase(idAsString, null, "Nilsson", ExpectedResult = false)]        
        [TestCase(idAsString, "Jimmy", null, ExpectedResult = false)]        
        public bool Then_it_is_equal_to_another_object_or_not_depending_on_the_values(string expectedId, string firstName, string lastName)
        {
            var anotherConsultant = new Consultant(new Guid(expectedId), firstName, lastName);
            return consultant.Equals(anotherConsultant);
        }
    }
}
