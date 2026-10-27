
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendDotNet.Application.Abstractions.Email;
public interface IEmailSender
{
    Task SendAsync(string subject, string body, CancellationToken ct = default);
}
