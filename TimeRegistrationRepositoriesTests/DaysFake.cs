using System;
using System.Collections.Generic;
using System.Linq;
using factor10.TimeRegistration.DomainModel;

namespace factor10.TimeRegistration.Repositories
{
    public class DaysFake: IDays
    {
        private readonly IDictionary<string, Day> days = new Dictionary<string, Day>();
        public void Save(Day day)
        {
            var key = getKey(day.Consultant, day.Date);
            if (days.ContainsKey(key))
                days[key] = day;
            else
                days.Add(key, day);    
        }

        private static string getKey(Consultant consultant, DateTime date)
        {
            if (consultant == null)
                return "";

            return consultant.Person.FullName + date;
        }

        public Day CertainDayForConsultant(Consultant consultant, DateTime date)
        {
            days.TryGetValue(getKey(consultant, date), out var day);
            return day;
        }

        public IEnumerable<Day> Between(DateTime from, DateTime to)
        {
            return days.Values.Where(d => d.Date >= from && d.Date <= to);
        }
    }
}