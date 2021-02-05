using System;
using System.IO;
using factor10.TimeRegistration.AntiCorruptionLayer;
using factor10.TimeRegistration.DomainModel;
using factor10.TimeRegistration.Repositories;
using LiteDB;
using NUnit.Framework;
using FluentAssertions;

namespace TimeRegistrationRepositoriesTests
{
    [TestFixture]
    public class When_saving_a_project
    {
        private readonly Projects projects = new Projects(RepositoryTestUtils.CreateNewDatabase());
        private Project project;
        private string randomName = "Architecture Refactoring X" + Guid.NewGuid();

        public When_saving_a_project()
        {
            var finnair = new CustomersAgent().TheOneWithName("Finnair");
            project = new Project(finnair, randomName);
            project.AddActivity("X");
            projects.Save(project);
        }

        [Test]
        public void Then_the_project_can_be_reconstituted()
        {
            var project2 = projects.TheOneWithName(project.Name);

            var expected = new {Name = randomName, Activities = new [] {"X"}};
            
            project2.Should().BeEquivalentTo(expected);
        }
    }
    
    public abstract class RepositoryTestUtils
    {
        public static LiteDatabase CreateNewDatabase()
        {
            var filename = Path.GetTempFileName();
            var connectionString = $"Filename={filename}.db; Mode=Exclusive";
            return new LiteDatabase(connectionString);
        }
    }
}
