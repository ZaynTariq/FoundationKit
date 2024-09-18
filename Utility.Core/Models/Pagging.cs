namespace Utility.Core.Models;

public class Pagging(int? pageSize, int? currentPage, string? search)
{
    public int Take
    {
        get
        {
            PageSize ??= 10;
            return PageSize.Value;
        }
    }

    public int Skip
    {
        get
        {
            CurrentPage ??= 1;
            return (CurrentPage.Value - 1) * Take;
        }
    }

    public int? PageSize { get; private set; } = pageSize;
    public int? CurrentPage { get; private set; } = currentPage;
    public string? Search { get; private set; } = search;
}

