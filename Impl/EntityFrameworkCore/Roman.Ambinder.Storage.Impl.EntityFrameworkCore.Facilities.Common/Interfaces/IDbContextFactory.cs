using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}