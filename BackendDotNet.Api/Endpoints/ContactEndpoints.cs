using System;
using System.Collections.Generic;
using System.Text;
using BackendDotNet.Application.Abstractions.Messaging;
using BackendDotNet.Application.Contacts.Commands;

namespace BackendDotNet.Api.Endpoints;

public sealed record CreateContactRequest(string Name, string PhoneNumber, string Reason);

public static class ContactEndpoints
{
    public static IEndpointRouteBuilder MapContactEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/Contact", async (
            CreateContactRequest request,
            IDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var command = new CreateContactCommand(
                request.Name,
                request.PhoneNumber,
                request.Reason);

            var result = await dispatcher.Send(command, ct);

            return result.IsSuccess
                ? Results.Created($"/Contact/{result.Value!.ContactId}", result.Value)
                : result.Error.ToProblem();
        })
        .WithName("CreateContact")
        .WithTags("Contact");

        return app;
    }
}