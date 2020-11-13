namespace Storage.Examples.AspCore.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public byte Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
    }
}
