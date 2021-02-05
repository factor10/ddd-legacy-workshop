using System;

namespace factor10.TimeRegistration.DomainModel
{
    public class Consultant : IEquatable<Consultant>
    {

        public Person Person { get; private set; }
        public Guid Id { get; private set; }

        public Consultant(Guid id, string firstName, string lastName)
        {
            Person = new Person(firstName, lastName);
            Id = id;
        }
        public Consultant(Guid id, string fullName)
        {
            Person = new Person(fullName);
            Id = id;
        }

        private Consultant() {} //Needed by the persistence solution

        #region Equality
        public bool Equals(Consultant other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Person, other.Person) && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Consultant) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Person != null ? Person.GetHashCode() : 0) * 397) ^ Id.GetHashCode();
            }
        }

        public static bool operator ==(Consultant left, Consultant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Consultant left, Consultant right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
    
    
}
