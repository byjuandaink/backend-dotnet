using BackendDotNet.Application.Abstractions.Messaging;
using BackendDotNet.Application.Abstractions.Persistence;
using BackendDotNet.Application.Common;
using BackendDotNet.Application.Contacts.Commands;
using BackendDotNet.Application.Contacts.Dtos;
using BackendDotNet.Domain.Entities;
using BackendDotNet.Application.Abstractions.Email;
using Microsoft.Extensions.Logging; 
using FluentValidation;

namespace BackendDotNet.Application.Contacts.Handlers;

public sealed class CreateContactCommandHandler
    : ICommandHandler<CreateContactCommand, ContactDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateContactCommand> _validator;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<CreateContactCommandHandler> _logger;

    public CreateContactCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateContactCommand> validator,
        IEmailSender emailSender,
        ILogger<CreateContactCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _emailSender = emailSender;
        _logger = logger;
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

        try
        {
            await _emailSender.SendAsync(
                subject: $"Nuevo contacto del portafolio: {contact.Name}",
                body: $"Nombre: {contact.Name}\nTeléfono: {contact.PhoneNumber}\nRazón: {contact.Reason}\nFecha (UTC): {contact.CreatedAt:u}",
                ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo enviar el correo del contacto {ContactId}", contact.ContactId);
        }
        try
        {
            await _emailSender.SendAsync(
                subject: $"Nuevo contacto del portafolio: {contact.Name}",
                body: $"Nombre: {contact.Name}\nTeléfono: {contact.PhoneNumber}\nRazón: {contact.Reason}\nFecha (UTC): {contact.CreatedAt:u}",
                ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo enviar el correo del contacto {ContactId}", contact.ContactId);
        }

        return Result<ContactDto>.Success(contact.ToDto());
    }
}