using System.Security.Claims;
using fh_app_back.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers.Abstract;

/// <summary>
/// Base controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseControllerApi : ControllerBase
{
   
    /// <summary>
    /// Logger
    /// </summary>
    protected ILogger Logger { get; }
    
    
    protected BaseControllerApi(ILogger logger)
    {
        Logger = logger;
    }
    
    
    /// <summary>
    /// Convert data to frontend response obj, if method throw exception back error message
    /// </summary>
    /// <param name="data">Response Data</param>
    /// <typeparam name="T">Type</typeparam>
    /// <returns>Frontend response</returns>
    protected IActionResult MakeResponse<T>(T? data)
    {
        return Ok(new BaseResponse<T>(data));
    }
}