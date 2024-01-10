using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Contract.Abstractions.Shared;
public class PagedResult<T>
{
    public const int UpperPageSize = 100;
    public const int DefaultPageIndex = 1;
    public const int DefaultPageSize = 10;

    private PagedResult(List<T> Items, int PageIndex, int PageSize, int TotalCount)
    {
        this.Items = Items;
        this.PageIndex = PageIndex;
        this.PageSize = PageSize;
        this.TotalCount = TotalCount;
    }

    public List<T> Items { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => PageIndex * PageSize <= TotalCount;
    public bool HasPreviousPage => PageIndex > 1;

    public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> query, int PageIndex, int PageSize)
    {
        PageIndex = PageIndex <= 0 ? DefaultPageIndex : PageIndex;
        PageSize = PageSize > 0
            ? PageSize > UpperPageSize
            ? UpperPageSize : PageSize : DefaultPageSize;

        var totalCount = await query.CountAsync();
        var items = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToListAsync();
        return new(items, PageIndex, PageSize, totalCount);
    }

    public static PagedResult<T> Create(List<T> items, int PageIndex, int PageSize, int totalCount)
        => new(items, PageIndex, PageSize, totalCount);
}
