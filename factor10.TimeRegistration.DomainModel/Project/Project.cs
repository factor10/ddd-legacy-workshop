using System;
using System.Collections.Generic;

namespace factor10.TimeRegistration.DomainModel
{
    public class Project : IEntity
    {
        private Project()
        {
            //Needed for the persistence solution...
            Activities = new List<string>();
            Consultants = new List<Consultant>();
        } 
        public Guid Id { get; private set;  }

        public string Name { get; private set; }
        public Customer Customer { get; private set; }
        public List<string> Activities { get; private set; }
        public List<Consultant> Consultants { get; private set; }

        public Project(Customer customer, string name) : this()
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Name = name;
        }
        
        public void AddActivity(string activity)
        {
            Activities.Add(activity);
        }

        public void AddConsultant(Consultant consultant)
        {
            Consultants.Add(consultant);
        }

        public bool IsReadyToGetTimeRegistrations => Consultants.Count > 0 && Activities.Count > 0;

        public ProjectSnapshot TakeSnapshot()
        {
            return new ProjectSnapshot(Name);
        }
    }
}
