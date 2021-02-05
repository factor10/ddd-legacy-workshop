using System;
using System.Collections.Generic;
using System.Linq;
using factor10.TimeRegistration.AntiCorruptionLayer;
using factor10.TimeRegistration.DomainModel;
using factor10.TimeRegistration.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace factor10.TimeRegistration.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultantsController : ControllerBase
    {
        private readonly Projects projects;

        public ConsultantsController(IDb db)
        {
            projects  = new Projects(db.One);
            
            //Create some data if not existing...
            var per = new ConsultantsAgent().TheOneWithFullName("Per Persson");
            
            var p = projects.ForConsultant(per.Id).ToList();
            if (p.Count == 0)
            {
                var customers = new CustomersAgent();
                var finnair = customers.TheOneWithName("Finnair");
                
                var project = new Project(finnair, "Finnair's new app");
                project.AddConsultant(per);
                projects.Save(project);
                
                var stina = new ConsultantsAgent().TheOneWithFullName("Stina Johansson");
                var bruce = new ConsultantsAgent().TheOneWithFullName("Bruce Wayne");

                project = new Project(finnair, "Yet another app");
                project.AddConsultant(per);
                project.AddConsultant(stina);
                project.AddConsultant(bruce);
                projects.Save(project);
            }
        }
        
        [HttpGet("{consultantId}/projects")]
        public IEnumerable<Project> GetForConsultant(Guid consultantId)
        {
            return projects.ForConsultant(consultantId);
        }
        
        [HttpGet]
        public IEnumerable<Consultant> Get()
        {
            var consultants = new ConsultantsAgent();
            return consultants.All();
        }
    }
}