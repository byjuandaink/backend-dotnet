using System;
using System.Collections.Generic;
using System.Text;

namespace BackendDotNet.Application.Contacts.Dtos;

public sealed record ContactDto(
    int ContactId,
    string Name,
    string PhoneNumber,
    string Reason,
    DateTime CreatedAt);