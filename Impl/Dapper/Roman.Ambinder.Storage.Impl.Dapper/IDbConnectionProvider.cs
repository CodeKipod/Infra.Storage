using System.Data.Common;

namespace Roman.Ambinder.Storage.Impl.Dapper
{
    public interface IDbConnectionProvider
    {
        DbConnection Get();
    }
}
