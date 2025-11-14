using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WatchList.Core.Tools.Concrete.Results;

public class BaseResponseModel 
{
    public object? Data { get; set; }
    public string? Description { get; set; } = String.Empty;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    
}