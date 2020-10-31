namespace Roman.Ambinder.Storage.Common.DataTypes
{
    public class PagingParams
    {
        public PagingParams(int currentPage, int itemsPerPage)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
        }

        public int CurrentPage { get; }
        public int ItemsPerPage { get; }
    }
}
