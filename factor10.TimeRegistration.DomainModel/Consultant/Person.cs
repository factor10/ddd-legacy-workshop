using System;

namespace factor10.TimeRegistration.DomainModel
{
    [Serializable]
    public class Person : IEquatable<Person>
    {
        public  string FirstName { get; private set; }
        public  string LastName { get; private set; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public Person(string fullName)
        {
            FirstName = fullName.Substring(0, fullName.IndexOf(" "));
            LastName = fullName.Substring(fullName.IndexOf(" ") + 1);
        }

        public string FullName => FirstName + " " + LastName;
        
        private Person() {} //Needed by the persistence solution


        #region Equality
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Person)) return false;
            return Equals((Person) obj);
        }

        public bool Equals(Person obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.FirstName, FirstName) && Equals(obj.LastName, LastName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FirstName != null ? FirstName.GetHashCode() : 0)*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Person left, Person right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
