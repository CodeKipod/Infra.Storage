using System;
using System.ComponentModel.DataAnnotations;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities
{
    public class CompsiteKeyPerson : IEquatable<CompsiteKeyPerson>
    {
        public CompsiteKeyPerson()
        {
        }

        public CompsiteKeyPerson(byte age, string firstName, string lastName)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(CompsiteKeyPerson other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Key1 == other.Key1 && Age == other.Age && FirstName == other.FirstName && LastName == other.LastName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CompsiteKeyPerson)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key1, Age, FirstName, LastName);
        }

        public int Key1 { get; private set; }
        public int Key2 { get; private set; }

        public int Key3 { get; private set; }

        public byte Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}