using Microsoft.EntityFrameworkCore;
using Storage.Examples.AspCore.Entities;

namespace Storage.Examples.AspCore.Repositories
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext()
        {

        }

        public PeopleDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Person> People { get; set; }
    }
}
