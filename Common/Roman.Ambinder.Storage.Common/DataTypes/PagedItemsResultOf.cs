using System.Collections.Generic;

namespace Roman.Ambinder.Storage.Common.DataTypes
{

    public class PagedItemsResultOf<T> : BasePagedResult
    {
        public IReadOnlyCollection<T> Items { get; set; }

        public PagedItemsResultOf(
            int currentPage,
            int itemsPerPage,
            int totalNumberOfItems)
            : base(currentPage: currentPage,
                itemsPerPage: itemsPerPage,
                totalNumberOfItems: totalNumberOfItems)
        { }

        public PagedItemsResultOf(IReadOnlyCollection<T> items)
            : base(currentPage: 1, itemsPerPage: items.Count, totalNumberOfItems: items.Count)
        {
            Items = items;
        }
    }
}
