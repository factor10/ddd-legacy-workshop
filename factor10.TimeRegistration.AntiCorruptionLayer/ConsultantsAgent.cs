using System;
using System.Collections.Generic;
using factor10.TimeRegistration.DomainModel;

namespace factor10.TimeRegistration.AntiCorruptionLayer
{
    public class ConsultantsAgent: IConsultantsAgent
    {
        //For now, fake a few well known consultants. Integration and/or persistent cache later on.
        static ConsultantsAgent()
        {
            var pelle = new Consultant(Guid.Parse("11edb330-6b82-bc0a-a509-00340fd71211"), "Per Persson");
            consultants.Add(pelle.Id, pelle);

            var stina = new Consultant(Guid.Parse("22edb330-6b82-bc0a-a509-00340fd71222"),"Stina Johansson");
            consultants.Add(stina.Id, stina);

            var bruce = new Consultant(Guid.Parse("33edb330-6b82-bc0a-a509-00340fd71233"),"Bruce Wayne");
            consultants.Add(bruce.Id, bruce);
        }

        private static readonly IDictionary<Guid, Consultant> consultants = new Dictionary<Guid, Consultant>();
        
        public Consultant TheOneWithFullName(string fullName)
        {
            Consultant result = null;
            foreach (var c in consultants.Values)
            {
                if (c.Person.FullName == fullName)
                    result = c;
            }
            return result;
        }
        public Consultant TheOneWithId(Guid id)
        {
            consultants.TryGetValue(id, out var consultant);
            return consultant;
        }

        public IEnumerable<Consultant> All()
        {
            return consultants.Values;
        }

    }
}
