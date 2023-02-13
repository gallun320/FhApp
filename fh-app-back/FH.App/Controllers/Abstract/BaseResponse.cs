using FH.Infrastructure.Exceptions;

namespace fh_app_back.Controllers.Abstract;

/// <summary>
/// Base response struct
/// </summary>
public class BaseResponse<T>
{
    /// <summary>
    /// Empty OK result
    /// </summary>
    public readonly static BaseResponse<bool?> Ok = new(null);

    /// <summary>
    /// Empty ERROR result
    /// </summary>
    public readonly static BaseResponse<bool?> Failed = new BaseResponse<bool?>("error");

    /// <summary>
    /// Success flag
    /// </summary>
    public bool Success => string.IsNullOrEmpty(Error);

    /// <summary>
    /// Error description
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public BaseResponse()
    {
        Error = string.Empty;
        Data = default;
    }

    /// <summary>
    /// Constructor OK response
    /// </summary>
    /// <param name="data">Response Data</param>
    public BaseResponse(T? data)
    {
        Error = string.Empty;
        Data = data;
    }

    /// <summary>
    /// Constructor ERROR response
    /// </summary>
    /// <param name="error">Error message</param>
    /// <param name="data">Data for error</param>
    public BaseResponse(string error, T? data = default)
    {
        Error = error;
        Data = data;
    }
    
    /// <summary>
    /// Constructor ERROR response
    /// </summary>
    /// <param name="error">Error message</param>
    /// <param name="data">Data for error</param>
    public BaseResponse(FhException error, T? data = default)
    {
        Error = error.Message;
        Data = data;
    }
    
    /// <summary>
    /// Response data
    /// </summary>
    public T? Data { get; set; }
}
