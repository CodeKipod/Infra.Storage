using NHibernate.Linq;
using Roman.Ambinder.Storage.Common.DataTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.NHibernate
{
    public static class QueryableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<PagedItemsResultOf<T>> ToPagedResultsArrayAsync<T>(
        this IQueryable<T> query,
        PagingParams pagingParams,
        CancellationToken cancellationToken = default)
        {
            PagedItemsResultOf<T> pagedResults;

            if (pagingParams == null)
            {
                var items = await query
                   .ToListAsync(cancellationToken)
                   .ConfigureAwait(false);

                pagedResults = new PagedItemsResultOf<T>(items);
            }
            else
            {
                var itemsToSkip = (pagingParams.CurrentPage - 1) * pagingParams.ItemsPerPage;

            

                var totalNumberOfItems = await query.CountAsync(cancellationToken);

                pagedResults = new PagedItemsResultOf<T>(
                    pagingParams.CurrentPage,
                    pagingParams.ItemsPerPage,
                    totalNumberOfItems);

                pagedResults.Items = await query.Skip(itemsToSkip)
                      .Take(pagingParams.ItemsPerPage)
                      .ToListAsync(cancellationToken);
            }

            return pagedResults;
        }
    }
}
