using System;
using System.Collections.Generic;
using System.Text;  
using BackendDotNet.Application.Contacts.Dtos;
using BackendDotNet.Domain.Entities;

namespace BackendDotNet.Application.Contacts;

public static class ContactMappings
{
    public static ContactDto ToDto(this Contact contact)
        => new(
            contact.ContactId,
            contact.Name,
            contact.PhoneNumber,
            contact.Reason,
            contact.CreatedAt);
}       