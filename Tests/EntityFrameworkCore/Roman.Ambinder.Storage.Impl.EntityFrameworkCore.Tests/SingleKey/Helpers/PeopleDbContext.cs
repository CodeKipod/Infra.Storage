using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Helpers
{
    public class PeopleDbContext : SingleEntityTypeDbContextOf<SingleKeyPerson>
    {
     
    }
}