using Microsoft.EntityFrameworkCore;

namespace ECom_wep_app.Models.Utilities
{
    public class PaginatedList<T> : IPaginated
    {
        public List<T> Items { get; }
        public int PageIndex { get; }           
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

        // ===== New: Async factory for EF Core IQueryable =====
        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var total = await source.CountAsync(cancellationToken);

            var totalPages = pageSize == 0 ? 0 : (int)Math.Ceiling(total / (double)pageSize);
            if (totalPages > 0 && pageIndex > totalPages)
                pageIndex = totalPages;

            var items = total == 0
                ? new List<T>()
                : await source
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, pageIndex, pageSize, total);
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var list = source.ToList();
            var total = list.Count;

            var totalPages = pageSize == 0 ? 0 : (int)Math.Ceiling(total / (double)pageSize);
            if (totalPages > 0 && pageIndex > totalPages)
                pageIndex = totalPages;

            var items = total == 0
                ? new List<T>()
                : list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, pageIndex, pageSize, total);
        }
    }
}
