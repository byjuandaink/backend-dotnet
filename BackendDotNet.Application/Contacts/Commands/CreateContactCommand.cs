    using System;
using System.Collections.Generic;
using System.Text;

using BackendDotNet.Application.Abstractions.Messaging;
using BackendDotNet.Application.Contacts.Dtos;

namespace BackendDotNet.Application.Contacts.Commands;

public sealed record CreateContactCommand(
    string Name,
    string PhoneNumber,
    string Reason) : ICommand<ContactDto>;