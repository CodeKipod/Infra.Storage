using System;

namespace Roman.Ambinder.Storage.Common.DataTypes
{
    public abstract class BasePagedResult
    {
        protected BasePagedResult(int currentPage,
            int itemsPerPage,
            int totalNumberOfItems)
        {
            CurrentPage = currentPage;

            if (itemsPerPage > 0)
                TotalNumberOfPages = (int)Math.Ceiling((double)totalNumberOfItems / itemsPerPage);

            ItemsPerPage = itemsPerPage;
            TotalNumberOfItems = totalNumberOfItems;
        }

        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalNumberOfItems { get; set; }

        public int FirstItemOnPage => (CurrentPage - 1) * ItemsPerPage + 1;

        public int LastItemOnPage => Math.Min(CurrentPage * ItemsPerPage, TotalNumberOfItems);
    }
}