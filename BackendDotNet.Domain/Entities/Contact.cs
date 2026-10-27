using System;
using System.Collections.Generic;
using System.Text;

namespace BackendDotNet.Domain.Entities;

public class Contact
{
    public int ContactId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}