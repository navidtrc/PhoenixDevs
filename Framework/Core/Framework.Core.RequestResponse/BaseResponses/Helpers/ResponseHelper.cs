using Microsoft.AspNetCore.Mvc;

namespace Framework.Core.RequestResponse.BaseResponses.Helpers;

public static class ResponseHelper
{
    public static IActionResult ToActionResult<T>(this ListResponseBase<T> response)
    {
        return response.Status ?
            new ObjectResult(response) :
            new BadRequestObjectResult(response);
    }

    public static IActionResult ToActionResult<T, TId>(this ListResponseBase<T, TId> response)
    {
        return response.Status ?
            new ObjectResult(response) :
            new BadRequestObjectResult(response);
    }

    public static IActionResult ToActionResult(this ResponseBase response)
    {
        return response ?
            new ObjectResult(response) :
            new BadRequestObjectResult(response);
    }

    public static IActionResult ToActionResult<T>(this ResponseBase<T> response)
    {
        return response.Status ?
            new ObjectResult(response) :
            new BadRequestObjectResult(response);
    }

   
    public static IActionResult ToActionResult(this ResponseStatus status)
    {
        return status ?
            new ObjectResult(ResponseBase.Success()) :
            new BadRequestObjectResult(ResponseBase.Failure(status));
    }
}