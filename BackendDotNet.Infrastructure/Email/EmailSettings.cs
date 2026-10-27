using System;
using System.Collections.Generic;
using System.Text;
namespace BackendDotNet.Infrastructure.Email;

public sealed class EmailSettings
{
    public const string SectionName = "Email";

    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;  
    public string Password { get; set; } = string.Empty;    
    public string To { get; set; } = string.Empty;        
}