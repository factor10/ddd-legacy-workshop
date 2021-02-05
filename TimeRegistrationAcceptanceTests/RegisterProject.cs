using System;
using factor10.TimeRegistration.DomainModel;
using NUnit.Framework;

namespace TimeRegistrationAcceptanceTests
{
    [TestFixture]
    public class RegisterProject
    {
        [Test]
        public void HappyCase()
        {
            //Please note that this was written before anything else existed. In a real situation it would probably be transformed now to use the different 
            //units for repositories and agents as well, just as the step definitions are.
            
            //Given customer Finnair exists
            var finnair = new Customer("Finnair");

            //When project Phone app is added for Finnair with activity and consultant
            var phoneApp = new Project(finnair, "Phone app");
            phoneApp.AddActivity("Coding");
            phoneApp.AddConsultant(new Consultant(Guid.NewGuid(), "Jimmy", "Nilsson"));

            //Then Phone app is ready to get time registrations
            Assert.IsTrue(phoneApp.IsReadyToGetTimeRegistrations);
        }
    }
}
