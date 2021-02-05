using System;
using System.Collections.Generic;

namespace factor10.TimeRegistration.DomainModel
{
    public interface IProjects
    {
        Project TheOneWithName(string name);

        void Save(Project entity);

        IEnumerable<Project> ForConsultant(Guid id);
    }
}