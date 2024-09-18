namespace Utility.Core.Results;

public class PagedResult<T>(T items, int count, int? currentPage = default!, int? pageSize = default!)
{

    public int CurrentPage { get; set; } = currentPage ?? 1;
    public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)(pageSize ?? 10));
    public int PageSize { get; set; } = pageSize ?? 10;
    public int TotalCount { get; set; } = count;
    public T Items { get; set; } = items;
}
