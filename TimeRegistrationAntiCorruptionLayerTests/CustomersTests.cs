using factor10.TimeRegistration.AntiCorruptionLayer;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationAntiCorruptionLayerTests
{
    [TestFixture]
    public class When_saving_a_customer
    {
        private readonly CustomersAgent customersAgent = new CustomersAgent();
        
        [Test]
        public void Then_the_customer_can_be_reconstituted()
        {
            var customer = customersAgent.TheOneWithName("Sonera");
            customer.Name.Should().Be("Sonera");
        }

        [Test]
        public void Then_customer_can_not_be_found_for_non_existing_name()
        {
            var customer = customersAgent.TheOneWithName("Volvo");
            customer.Should().BeNull();
        }
    }
}
