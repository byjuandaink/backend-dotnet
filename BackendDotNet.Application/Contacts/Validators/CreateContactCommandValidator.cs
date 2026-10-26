
using System;
using System.Collections.Generic;
using System.Text;
using BackendDotNet.Application.Contacts.Commands;
using FluentValidation;

namespace BackendDotNet.Application.Contacts.Validators;

public sealed class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar 100 caracteres.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de contacto es obligatorio.")
            .MaximumLength(20).WithMessage("El número no puede superar 20 caracteres.")
            .Matches(@"^\+?[0-9\s\-]{7,20}$")
            .WithMessage("El número solo puede contener dígitos, espacios, guiones y un + inicial.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("La razón de comunicación es obligatoria.")
            .MaximumLength(1000).WithMessage("La razón no puede superar 1000 caracteres.");
    }
}