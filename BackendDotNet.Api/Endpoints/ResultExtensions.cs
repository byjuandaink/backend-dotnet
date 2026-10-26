using BackendDotNet.Application.Common;

namespace BackendDotNet.Api.Endpoints;

public static class ResultExtensions
{
    public static IResult ToProblem(this Error error)
    {
        var status = error.Code switch
        {
            "NotFound" => StatusCodes.Status404NotFound,
            "Validation" => StatusCodes.Status400BadRequest,
            "Conflict" => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(
            title: error.Code,
            detail: error.Message,
            statusCode: status);
    }
}