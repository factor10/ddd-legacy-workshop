using factor10.TimeRegistration.AntiCorruptionLayer;
using factor10.TimeRegistration.DomainModel;
using FluentAssertions;
using NUnit.Framework;

namespace TimeRegistrationAntiCorruptionLayerTests
{
    [TestFixture]
    public class When_asking_for_a_consultant_from_the_domainservice
    {
        private readonly IConsultantsAgent consultantsAgent = new ConsultantsAgent();

        [Test]
        public void Then_the_consultant_can_be_reconstituted()
        {
            var consultant = consultantsAgent.TheOneWithFullName("Stina Johansson");
            var expected = new {Person = new {FirstName = "Stina", LastName = "Johansson"}};
            consultant.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Then_consultant_can_not_be_found_for_non_existing_name()
        {
            var consultant = consultantsAgent.TheOneWithFullName("X Y");
            consultant.Should().BeNull();
        }
    }
}
