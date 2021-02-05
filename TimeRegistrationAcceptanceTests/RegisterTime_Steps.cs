using System;
using System.Linq;
using factor10.TimeRegistration.AntiCorruptionLayer;
using factor10.TimeRegistration.DomainModel;
using factor10.TimeRegistration.Repositories;
using FluentAssertions;
using LiteDB;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TimeRegistrationAcceptanceTests
{
    [Binding]
    public class RegisterTime_Steps
    {
        private readonly LiteDatabase db = RepositoryTestUtils.CreateNewDatabase();
        private readonly Projects projects;
        private readonly IConsultantsAgent consultantsAgent = new ConsultantsAgent();
        private readonly Days days;

        public RegisterTime_Steps()
        {
            projects = new Projects(db);
            days = new Days(db);
        }
            
        
        [Given("consultant (.*) exists")]
        public void Given_consultant_X_exists(string fullName)
        {
            Assert.IsNotNull(consultantsAgent.TheOneWithFullName(fullName));
        }

        [Given("project (.*) exists and is ready for registration")]
        public void Given_project_X_exists_and_is_ready_for_registration(string projectName)
        {
            var project = projects.TheOneWithName(projectName);
            if (project == null) { 
                project = new Project(new Customer("Volvo"), projectName);
                project.AddActivity("Testing");
                project.AddConsultant(new Consultant(Guid.NewGuid(), "Svea", "Svensson"));
                projects.Save(project);
            }
        }

        [Given("today has (.*) hours")]
        public void Given_today_has_X_hours(int hours)
        {
            //This expects the database to be empty before each execution... It would be more inline with the story to let "Between()" use Customer also as parameter...
            checkTheSumOfHoursToday(hours);
        }


        [When("(.*) registers (.*) hours of (.*) on project (.*) for today")]
        public void When_X_registers_Y_hours_of_Z_on_project_K(string consultantName, int hours, string activity, string projectName)
        {
            var consultant = consultantsAgent.TheOneWithFullName(consultantName);
            var day = days.CertainDayForConsultant(consultant, DateTime.Today);
            if (day == null)
                day = new Day(consultant, DateTime.Today);

            var project = projects.TheOneWithName(projectName);
            day.AddRegistration(new Registration(new Duration(hours * 60), activity, project));

            days.Save(day);
        }

        [Then("today has (.*) hours")]
        public void Then_today_has_X_hours(int hours)
        {
            checkTheSumOfHoursToday(hours);
        }

        private void checkTheSumOfHoursToday(int hours)
        {
            hours.Should().Be(days.Between(DateTime.Today, DateTime.Today).Sum(d => d.Hours));
        }    
    }
}