using Microsoft.EntityFrameworkCore;

namespace Application.Common.Paging
{
    public class PagedList<TEntity> : List<TEntity>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<TEntity> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PagedList<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageNumber, int pageSize)
        {
            int count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<TEntity>(items, count, pageNumber, pageSize);
        }
    }
}
