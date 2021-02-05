using System;
using factor10.TimeRegistration.AntiCorruptionLayer;
using factor10.TimeRegistration.DomainModel;
using factor10.TimeRegistration.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace factor10.TimeRegistration.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaysController : ControllerBase
    {
        private readonly IDb db;

        public DaysController(IDb db)
        {
            this.db = db;
        }
        
        [HttpGet("{consultantId}/{date}/registrations")]
        public Day Get(Guid consultantId, DateTime date)
        {
            var consultants = new ConsultantsAgent();
            var consultant = consultants.TheOneWithId(consultantId);
            
            var days = new Days(db.One);
            var day = days.CertainDayForConsultant(consultant, date);
            if (day == null)
            {
                day = new Day(consultant, date);
            }

            return day;
        }
        

        [HttpPost("{consultantId}/{date}/registrations")]
        public void Post(Guid consultantId, DateTime date, [FromBody] RegistrationDTO registrationDTO)
        {
            var consultants = new ConsultantsAgent();
            var consultant = consultants.TheOneWithId(consultantId);
            
            var days = new Days(db.One);
            var day = days.CertainDayForConsultant(consultant, date);
            if (day == null)
            {
                day = new Day(consultant, date);
            }

            var projects = new Projects(db.One);
            var project = projects.TheOneWithName(registrationDTO.projectName);
            var registration = new Registration(Duration.Create(registrationDTO.duration), registrationDTO.activity, project);
            day.AddRegistration(registration);
            days.Save(day);
        }
    }

    public class RegistrationDTO
    {
        public string projectName { get; set; }
        public string activity { get; set; }
        public string duration { get; set; }
    }
}