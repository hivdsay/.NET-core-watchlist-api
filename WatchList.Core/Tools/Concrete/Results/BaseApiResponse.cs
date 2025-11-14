using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;



namespace WatchList.Core.Tools.Concrete.Results;

public class BaseWrapperController : ControllerBase
{
    string? RequestId { get { return HttpContext.Items["RequestId"]?.ToString(); } }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult OkResponse(object? result = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, result);

        return StatusCode((int)HttpStatusCode.OK, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult NotAuthenticatedResponse(string errorDescription)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, HttpStatusCode.Unauthorized.ToString(), errorDescription);

        return StatusCode((int)HttpStatusCode.Unauthorized, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult CreatedResponse(object? result = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, result);

        return StatusCode((int)HttpStatusCode.Created, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult AcceptedResponse(object? result = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, result);

        return StatusCode((int)HttpStatusCode.Accepted, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult NoContentResponse(object? result = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, result);

        return StatusCode((int)HttpStatusCode.OK, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult BadRequestResponse(string? error = null, string? errorDescription = null, List<ValidationError>? validationErrors = null)
    {
        if (string.IsNullOrEmpty(error))
            error = HttpStatusCode.BadRequest.ToString();

        ApiResponse apiResponse = new ApiResponse(RequestId, error, errorDescription, validationErrors);

        return StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult BadRequestResponse(string? errorDescription = null, List<ValidationError>? validationErrors = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, HttpStatusCode.BadRequest.ToString(), errorDescription, validationErrors);

        return StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult BadRequestResponse(string errorDescription)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, HttpStatusCode.BadRequest.ToString(), errorDescription);

        return StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult UnauthorizedResponse(string? errorDescription = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, HttpStatusCode.Unauthorized.ToString(), errorDescription);

        return StatusCode((int)HttpStatusCode.Unauthorized, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ForbiddenResponse(string? errorDescription = null)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, HttpStatusCode.Forbidden.ToString(), errorDescription);

        return StatusCode((int)HttpStatusCode.Forbidden, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult NotFoundResponse(string? error = null, string? errorDescription = null)
    {
        if (string.IsNullOrEmpty(error))
            error = HttpStatusCode.OK.ToString();

        ApiResponse apiResponse = new ApiResponse(RequestId, error, errorDescription);

        return StatusCode((int)HttpStatusCode.OK, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult NotFoundResponse(string? errorDescription)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, HttpStatusCode.OK.ToString(), errorDescription);

        return StatusCode((int)HttpStatusCode.OK, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ErrorResponse(string? errorMessage = null, string? errorDescription = null)
    {
        if (string.IsNullOrEmpty(errorMessage))
            errorMessage = HttpStatusCode.InternalServerError.ToString();

        ApiResponse apiResponse = new ApiResponse(RequestId, errorMessage, errorDescription);

        return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ApiResponse(int statusCode, object result)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, result);

        return StatusCode(statusCode, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ApiResponse(HttpStatusCode httpStatusCode, object result)
    {
        ApiResponse apiResponse = new ApiResponse(RequestId, result);

        return StatusCode((int)httpStatusCode, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ApiResponse(int statusCode, string? errorMessage = null, string? errorDescription = null, List<ValidationError>? validationErrors = null)
    {
        if (string.IsNullOrEmpty(errorMessage))
            errorMessage = HttpStatusCode.InternalServerError.ToString();

        ApiResponse apiResponse = new ApiResponse(RequestId, errorMessage, errorDescription, validationErrors);

        return StatusCode(statusCode, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ApiResponse(HttpStatusCode httpStatusCode, string? errorMessage = null, string? errorDescription = null, List<ValidationError>? validationErrors = null)
    {
        if (string.IsNullOrEmpty(errorMessage))
            errorMessage = HttpStatusCode.InternalServerError.ToString();

        ApiResponse apiResponse = new ApiResponse(RequestId, errorMessage, errorDescription, validationErrors);

        return StatusCode((int)httpStatusCode, apiResponse);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult BaseApiResponse(BaseResponseModel responseModel, List<ValidationError>? validationErrors = null)
    {
        switch (responseModel.StatusCode)
        {
            case HttpStatusCode.OK:
                return OkResponse(responseModel.Data);
            case HttpStatusCode.Accepted:
                return AcceptedResponse(responseModel.Data);
            case HttpStatusCode.NotFound:
                return NotFoundResponse(responseModel.Description, responseModel.Description);
            case HttpStatusCode.Created:
                return CreatedResponse(responseModel.Data);
            case HttpStatusCode.BadRequest:
                return BadRequestResponse(responseModel.Description, responseModel.Description, validationErrors);
            case HttpStatusCode.Forbidden:
                return ForbiddenResponse(responseModel.Description);
            case HttpStatusCode.NoContent:
                return NoContentResponse(responseModel.Data);
        }

        if (string.IsNullOrEmpty(responseModel.Description))
            responseModel.Description = responseModel.StatusCode.ToString();

        ApiResponse apiResponse = new ApiResponse(RequestId, responseModel.Description, responseModel.Description, validationErrors);

        return StatusCode((int)responseModel.StatusCode, apiResponse);
    }

}