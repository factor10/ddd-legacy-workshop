using System;
using factor10.TimeRegistration.AntiCorruptionLayer;
using factor10.TimeRegistration.DomainModel;
using factor10.TimeRegistration.Repositories;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TimeRegistrationAcceptanceTests
{
    [Binding]
    public class RegisterProject_Steps
    {
        private readonly Projects projects = new Projects(RepositoryTestUtils.CreateNewDatabase());
        private readonly CustomersAgent customersAgent = new CustomersAgent();

        private readonly string randomSuffix = Guid.NewGuid().ToString();
        
        [Given("customer (.*) exists")]
        public void Given_customer_X_exists(string customerName)
        {
            Assert.IsNotNull(customersAgent.TheOneWithName(customerName));
        }

        [When("project (.*) is added for (.*) with activity and consultant")]
        public void When_project_X_is_added_for_Y_with_activity_and_consultant(string projectName, string customerName)
        {
            var customer = customersAgent.TheOneWithName(customerName);
            
            var project = new Project(customer, projectName+randomSuffix);
            project.AddActivity("Testing");
            project.AddConsultant(new Consultant(Guid.NewGuid(), "Svea", "Svensson"));
            projects.Save(project);
        }

        [Then("(.*) is ready to get time registrations")]
        public void Then_X_is_ready_to_get_time_registrations(string projectName)
        {
            var project = projects.TheOneWithName(projectName + randomSuffix);
            Assert.IsTrue(project.IsReadyToGetTimeRegistrations);
        }
    }
}