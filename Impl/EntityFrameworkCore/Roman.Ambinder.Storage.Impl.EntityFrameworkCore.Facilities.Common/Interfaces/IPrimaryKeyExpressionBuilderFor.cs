using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Common.Interfaces;

namespace Roman.Ambinder.Storage.EntityFrameworkCore.Facilities.Common
{
    public interface IPrimaryKeyExpressionBuilder : IPrimaryKeyExpressionBuilderFor<DbContext>
    {
     
    }
}