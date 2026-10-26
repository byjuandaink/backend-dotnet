using BackendDotNet.Application.Abstractions.Messaging;
using BackendDotNet.Application.Abstractions.Persistence;
using BackendDotNet.Application.Common;
using BackendDotNet.Application.Contacts.Commands;
using BackendDotNet.Application.Contacts.Dtos;
using BackendDotNet.Domain.Entities;
using FluentValidation;

namespace BackendDotNet.Application.Contacts.Handlers;

public sealed class CreateContactCommandHandler
    : ICommandHandler<CreateContactCommand, ContactDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateContactCommand> _validator;

    public CreateContactCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateContactCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<ContactDto>> Handle(CreateContactCommand command, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(command, ct);
        if (!validation.IsValid)
        {
            var message = string.Join(" ", validation.Errors.Select(e => e.ErrorMessage));
            return Result<ContactDto>.Failure(Error.Validation(message));
        }

        var contact = new Contact
        {
            Name = command.Name.Trim(),
            PhoneNumber = command.PhoneNumber.Trim(),
            Reason = command.Reason.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _unitOfWork.Contacts.Add(contact);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<ContactDto>.Success(contact.ToDto());
    }
}