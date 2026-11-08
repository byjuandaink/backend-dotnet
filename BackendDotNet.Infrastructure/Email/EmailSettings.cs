using System;
using System.Collections.Generic;
using System.Text;
namespace BackendDotNet.Infrastructure.Email;

public sealed class EmailSettings
{
    public const string SectionName = "Email";

    public string ApiKey { get; set; } = string.Empty;
    public string From { get; set; } = "onboarding@resend.dev";
    public string To { get; set; } = string.Empty;
}