using Utility.Core.Constants;

namespace Utility.Core.Results;

public class ApplicationResult<T>
{
    public ResponseStatus Status { get; set; }
    public string Message { get; set; } = default!;
    public T Response { get; set; } = default!;
    public string ErrorCode { get; set; } = default!;// Optional, useful for specific error handling
    public Exception? Exception { get; set; }
    // Pagination properties
    public int? CurrentPage { get; set; }
    public int? TotalPages { get; set; }
    public int? PageSize { get; set; }
    public int? TotalCount { get; set; }

    public ApplicationResult()
    {
    }

    public ApplicationResult(
        ResponseStatus status,
        string message,
        T data = default!,
        string errorCode = null!,
        int? currentPage = null,
        int? totalPages = null,
        int? pageSize = null,
        int? totalCount = null)
    {
        Status = status;
        Message = message;
        Response = data;
        ErrorCode = errorCode;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    // Implicit conversion from T (data) to ApiResponse<T>
    public static implicit operator ApplicationResult<T>(T data)
    {
        return new ApplicationResult<T>
        {
            Status = ResponseStatus.Success,
            Response = data,
            Message = "Operation successful."
        };
    }

    // Implicit conversion from string (error message) to ApiResponse<T>
    public static implicit operator ApplicationResult<T>(string errorMessage)
    {
        return new ApplicationResult<T>
        {
            Status = ResponseStatus.Error,
            Message = errorMessage
        };
    }

    // Implicit conversion for general exceptions
    public static implicit operator ApplicationResult<T>(Exception ex)
    {
        var message = ex.Message;

        if (ex.InnerException != null)
        {
            message += $"\n || {ex.InnerException.Message}";
        }

        return new ApplicationResult<T>
        {
            Status = ResponseStatus.Exception,
            Message = message,
            ErrorCode = "500"
        };
    }


    public static implicit operator ApplicationResult<T>(PagedResult<T> pagedResult)
    {
        return new ApplicationResult<T>
        {
            Status = ResponseStatus.Success,
            Response = pagedResult.Items,
            CurrentPage = pagedResult.CurrentPage,
            TotalPages = pagedResult.TotalPages,
            PageSize = pagedResult.PageSize,
            TotalCount = pagedResult.TotalCount,
            Message = "Operation successful."
        };
    }
}
