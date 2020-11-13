using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl
{
    public class SingleInstanceDbContextProvider :
        BaseDbContextProvider
    {
        private readonly DbContext _dbContext;

        public SingleInstanceDbContextProvider(IDbContextFactory dbContextFactory)
            : base(dbContextFactory, disposeAfterUsage: false)
        {
            dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));

            _dbContext = dbContextFactory.Create() ??
                throw new ArgumentNullException(nameof(_dbContext));
        }

        public override DbContext Get() => _dbContext;
    }
}