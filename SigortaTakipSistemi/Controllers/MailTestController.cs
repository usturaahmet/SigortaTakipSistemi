using Microsoft.AspNetCore.Mvc;

public class MailTestController : Controller
{
    private readonly IEmailService _mail;
    public MailTestController(IEmailService mail) => _mail = mail;

    //MailTest/Send?to=ornek@ornek.com
    [HttpGet]
    public async Task<IActionResult> Send(string to)
    {
        if (string.IsNullOrWhiteSpace(to))
            return BadRequest("to zorunlu");

        await _mail.SendAsync(
            to,
            "Test Maili",
            "<h3>Merhaba</h3><p>Sigorta Takip Programı'ndan test mailidir.</p>"
        );

        return Content($"Gönderildi → {to}");
    }
}
