using System;
using System.Collections.Generic;
using System.Linq;
using factor10.TimeRegistration.DomainModel;
using LiteDB;

namespace factor10.TimeRegistration.Repositories
{
    public class Days: BaseRepository<Day>, IDays
    {
        public Days(LiteDatabase db)
            : base(db)
        {
            Collection.EnsureIndex(d => d.Consultant.Id);
            Collection.EnsureIndex(d => d.Date);
        }

        public void Save(Day entity) =>
            BaseSave(entity);

        public Day CertainDayForConsultant(Consultant consultant, DateTime date)
        {
            return BaseAll().FindOne(d => d.Consultant.Id == consultant.Id && d.Date == date);
        }
        
        public IEnumerable<Day> Between(DateTime from, DateTime to)
        {
            return BaseAll().Find(d => d.Date >= from && d.Date <= to);
        }
    }
}
