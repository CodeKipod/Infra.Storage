using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey;
using Storage.Examples.AspCore.Entities;

namespace Storage.Examples.AspCore.Repositories
{
    public class EFCorePeopleRepository : 
        EFCoreSingleKeyRepositoryFor<int, Person, PeopleDbContext>
    {
        public EFCorePeopleRepository(
            PeopleDbContext dbContext)
            : base(dbContext)
        { }
    }
}
