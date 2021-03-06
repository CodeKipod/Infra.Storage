using System;
using System.ComponentModel.DataAnnotations;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities
{
    public class SingleKeyPerson : IEquatable<SingleKeyPerson>
    {
        public SingleKeyPerson()
        {
        }

        public SingleKeyPerson(byte age, string firstName, string lastName)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(SingleKeyPerson other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Age == other.Age && FirstName == other.FirstName && LastName == other.LastName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SingleKeyPerson)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Age, FirstName, LastName);
        }

        [Key]
        public int Id { get; private set; }

        public byte Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}