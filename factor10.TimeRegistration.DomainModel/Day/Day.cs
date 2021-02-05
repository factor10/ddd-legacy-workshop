using System;
using System.Collections.Generic;
using System.Linq;

namespace factor10.TimeRegistration.DomainModel
{
    public class Day : IEntity
    {
        private Day(){} //For persistence reasons
        public Guid Id { get; private set;  }

        public List<Registration> Registrations { get; private set; }
        
        public Consultant Consultant { get; private set; }
        public DateTime Date { get; private set; }

        public Day(Consultant consultant, DateTime date)
        {
            Registrations = new List<Registration>();
            Consultant = consultant;
            Date = date;
        }

        public int Hours
        {
            get
            {
                return Registrations.Sum(r => r.Duration.Hours); 
            }
        }

        public void AddRegistration(Registration registration)
        {
            Registrations.Add(registration);
        }
    }
}
