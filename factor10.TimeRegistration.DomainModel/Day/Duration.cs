using System;
using System.Dynamic;
using System.Globalization;

namespace factor10.TimeRegistration.DomainModel
{
    public class Duration
    {
        public int Minutes { get; private set; }

        public int Hours
        {
            get { return Minutes/60; }
        }

        public Duration(int minutes)
        {
            Minutes = minutes;
        }

        public static Duration Create(string input)
        {
            var minutes = 0;
            if (input.Contains("min"))
            {
                minutes = int.Parse(input.Replace("min",""));
            }
            else if (input.Contains("."))
            {
                minutes = (int)(decimal.Parse(input, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US")) * 60);
            }
            else if (input.Contains(":"))
            {
                var parts = input.Split(":");
                minutes = int.Parse(parts[0]) * 60 + int.Parse(parts[1]);
            }
            else
            {
                throw new Exception("This format is not understood: " + input);
            }
            return  new Duration(minutes);
        }
        
        private Duration() {} //For persistence reasons
    }
}