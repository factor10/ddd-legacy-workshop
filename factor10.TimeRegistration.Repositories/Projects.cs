using System;
using System.Collections.Generic;
using LiteDB;
using factor10.TimeRegistration.DomainModel;

namespace factor10.TimeRegistration.Repositories
{
    public class Projects : BaseRepository<Project>, IProjects
    {
        public Projects(LiteDatabase db)
            : base(db)
        {
            Collection.EnsureIndex(p=> p.Name);
        }

        public Project TheOneWithName(string name) =>
            BaseAll().FindOne(p => p.Name == name);

        public void Save(Project entity) =>
            BaseSave(entity);

        public IEnumerable<Project> ForConsultant(Guid id)
        {
            //return BaseAll().Find(p => p.Consultants.id == id);
            return BaseAll().Find(p => p.Consultants.Exists(c => c.Id == id));
        }
    }
}