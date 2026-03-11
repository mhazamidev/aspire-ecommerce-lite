using MediatR;
using Microsoft.AspNetCore.Http;

namespace Api.Shared.Base;

public abstract class BaseModule
{
    private readonly ISender _sender;

    protected BaseModule(ISender sender)
    {
        _sender = sender;
    }

    protected async Task<IResult> Response<TResponse>(IRequest<TResponse> request)
    {
        try
        {
            var result = await _sender.Send(request);
            return Results.Ok(new
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new
            {
                Success = false,
                Error = ex.Message
            });
        }
    }
}