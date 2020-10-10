using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}