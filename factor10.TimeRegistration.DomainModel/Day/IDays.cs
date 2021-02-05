using System;
using System.Collections.Generic;

namespace factor10.TimeRegistration.DomainModel
{
    public interface IDays
    {
        void Save(Day day);
        Day CertainDayForConsultant(Consultant consultant, DateTime date);
        IEnumerable<Day> Between(DateTime from, DateTime to);
    }
}