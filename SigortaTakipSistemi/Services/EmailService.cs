using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

public class MailSettings
{
    public string Host { get; set; } = "";
    public int Port { get; set; } = 465;
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string From { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public bool UseSSL { get; set; } = true;
}

public interface IEmailService
{
    Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default);
}

public class EmailService : IEmailService
{
    private readonly MailSettings _s;
    public EmailService(IOptions<MailSettings> options) => _s = options.Value;

    public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
    {
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress(_s.DisplayName, _s.From));
        msg.To.Add(MailboxAddress.Parse(to));
        msg.Subject = subject;
        msg.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

        using var smtp = new SmtpClient();

        var secure = _s.UseSSL ? SecureSocketOptions.SslOnConnect
                               : SecureSocketOptions.StartTlsWhenAvailable;

        await smtp.ConnectAsync(_s.Host, _s.Port, secure, ct);
        await smtp.AuthenticateAsync(_s.UserName, _s.Password, ct);
        await smtp.SendAsync(msg, ct);
        await smtp.DisconnectAsync(true, ct);
    }
}
