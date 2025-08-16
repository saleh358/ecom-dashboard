namespace ECom_wep_app.Models.Utilities
{
    public class PaginatedList<T> : IPaginated
    {
        public List<T> Items { get; }
        public int PageIndex { get; }           // 1-based
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize < 1 ? 10 : pageSize;
            TotalCount = totalCount < 0 ? 0 : totalCount;
            TotalPages = PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
        }
    }
}
