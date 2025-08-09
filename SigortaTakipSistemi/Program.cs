using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext (connection string'i appsettings.json > ConnectionStrings:DefaultConnection'dan okur)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // ÖNEMLÝ: Authorization'dan önce
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// (Opsiyonel) Ýlk admin oluþturma — istersen kullan
/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    if (!db.Kullanicilar.Any())
    {
        db.Kullanicilar.Add(new SigortaTakipSistemi.Models.Kullanici
        {
            IsimSoyisim = "Sistem Admin",
            KullaniciAdi = "admin",
            Eposta = "admin@example.com",
            Sifre = "12345", // üretimde hash'e çevir
            Rol = "Admin"
        });
        db.SaveChanges();
    }
}
*/

app.Run();
