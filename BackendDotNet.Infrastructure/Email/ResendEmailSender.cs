    using System.Net.Http.Headers;
using System.Net.Http.Json;
using BackendDotNet.Application.Abstractions.Email;
using Microsoft.Extensions.Options;

namespace BackendDotNet.Infrastructure.Email;

internal sealed class ResendEmailSender : IEmailSender
{
    private readonly HttpClient _http;
    private readonly EmailSettings _settings;

    public ResendEmailSender(HttpClient http, IOptions<EmailSettings> options)
    {
        _http = http;
        _settings = options.Value;
    }

    public async Task SendAsync(string subject, string body, CancellationToken ct = default)
    {
        var payload = new
        {
            @from = _settings.From,
            to = new[] { _settings.To },
            subject,
            text = body
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "emails")
        {
            Content = JsonContent.Create(payload)
        };
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        using var response = await _http.SendAsync(request, ct);

        if (!response.IsSuccessStatusCode)
        {
            var detail = await response.Content.ReadAsStringAsync(ct);
            throw new InvalidOperationException(
                $"Resend respondió {(int)response.StatusCode}: {detail}");
        }
    }
}
