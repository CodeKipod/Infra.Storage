using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces
{
    public interface IPrimaryKeyExpressionBuilder : IPrimaryKeyExpressionBuilderFor<DbContext>
    {
     
    }
}