using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Entities;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers
{
    public class CompositeKeyPeopleDbContext :
        SingleEntityTypeDbContextOf<CompsiteKeyPerson>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompsiteKeyPerson>()
                 .HasKey(c => new { c.Key1, c.Key2, c.Key3 });
        }
    }
}