using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationDomainModelTests
{
    [TestFixture]
    public class When_creating_a_customer
    {
        private Customer customer;
        
        [SetUp]
        public void SetUp()
        {
            customer = new Customer("Nokia");
        }

        [Test]
        public void Then_the_name_is_set()
        {
            customer.Name.Should().Be("Nokia");
        }
        
        [TestCase("Nokia", ExpectedResult = true)]
        [TestCase("Ericsson", ExpectedResult = false)]        
        [TestCase(null, ExpectedResult = false)]
        public bool Then_it_is_equal_to_another_object_or_not_depending_on_the_values(string name)
        {
            var anotherCustomer = new Customer(name);
            return customer.Equals(anotherCustomer);
        }
        
    }
}
