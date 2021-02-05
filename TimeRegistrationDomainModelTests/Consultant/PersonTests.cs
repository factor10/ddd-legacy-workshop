using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class When_creating_a_person
    {
        private Person person;

        [SetUp]
        public void SetUp()
        {
            person = new Person("Jimmy", "Nilsson");
        }

        [Test]
        public void Then_the_names_are_set()
        {
            var expected = new {FirstName = "Jimmy", LastName = "Nilsson"};
            person.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Then_the_full_name_is_set()
        {
            person.FullName.Should().Be("Jimmy Nilsson");
        }

        [Test]
        public void Then_the_object_is_equal_to_one_created_with_fullname()
        {
            var anotherPerson = new Person("Jimmy Nilsson");
            Assert.That(person.Equals(anotherPerson));
        }
        
        [TestCase("Jimmy", "Nilsson", ExpectedResult = true)]
        [TestCase("???", "Nilsson", ExpectedResult = false)]        
        [TestCase("Jimmy", "???", ExpectedResult = false)]
        [TestCase(null, "Nilsson", ExpectedResult = false)]
        [TestCase("Jimmy", null, ExpectedResult = false)]
        public bool Then_it_is_equal_to_another_object_or_not_depending_on_the_values(string firstName, string lastName)
        {
            var anotherPerson = new Person(firstName, lastName);
            return person.Equals(anotherPerson);
        }
    }
}
