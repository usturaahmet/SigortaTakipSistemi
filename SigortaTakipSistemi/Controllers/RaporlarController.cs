using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Data;
using SigortaTakipSistemi.Models.ViewModels;

namespace SigortaTakipSistemi.Controllers
{
    public class RaporlarController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public RaporlarController(ApplicationDbContext ctx) => _ctx = ctx;

        // Menü -> Raporlar
        [HttpGet]
        public IActionResult Index(int gun = 30) => RedirectToAction(nameof(Vade), new { gun });

        // Vade raporu (dolmuş + yaklaşan)
        [HttpGet]
        public async Task<IActionResult> Vade(int gun = 30)
        {
            var today = DateTime.Today;
            var cutoff = today.AddDays(gun);

            var raw = await _ctx.Policeler
                .Include(p => p.SigortaSirketi)
                .Include(p => p.PoliceTuru)
                .Select(p => new PoliceVadeDto
                {
                    Id = p.Id,
                    PoliceNo = p.PoliceNo ?? "",
                    Kisi = p.Kisi ?? "",
                    TcNo = p.TcNo,
                    TelefonNo = p.TelefonNo,
                    SigortaSirketi = p.SigortaSirketi != null ? p.SigortaSirketi.Ad : "",
                    PoliceTuru = p.PoliceTuru != null ? p.PoliceTuru.Ad : "",
                    Tanzim = p.TanzimTarihi,
                    Baslangic = p.BaslangicTarihi,
                    SureAy = p.PoliceSuresi,
                    Bitis = p.BaslangicTarihi.AddMonths(p.PoliceSuresi),
                    Fiyat = p.Fiyat
                })
                .Where(x => x.Bitis <= cutoff)      // sadece eşik içindekiler
                .OrderBy(x => x.Bitis)
                .ToListAsync();

            foreach (var x in raw)
            {
                x.KalanGun = (x.Bitis - today).Days;
                x.Durum = x.Bitis < today ? "Süresi Doldu" : "Yaklaşıyor";
            }

            var vm = new VadeRaporViewModel
            {
                Gun = gun,
                Dolanlar = raw.Where(x => x.Bitis < today).ToList(),
                Yaklasanlar = raw.Where(x => x.Bitis >= today).ToList()
            };

            ViewData["Title"] = "Poliçe Vade Raporu";
            return View(vm);
        }

        // CSV dışa aktar
        [HttpGet]
        public async Task<FileResult> VadeCsv(int gun = 30)
        {
            var today = DateTime.Today;
            var cutoff = today.AddDays(gun);

            var list = await _ctx.Policeler
                .Include(p => p.SigortaSirketi)
                .Include(p => p.PoliceTuru)
                .Select(p => new
                {
                    p.PoliceNo,
                    p.Kisi,
                    p.TcNo,
                    p.TelefonNo,
                    Sirket = p.SigortaSirketi != null ? p.SigortaSirketi.Ad : "",
                    Tur = p.PoliceTuru != null ? p.PoliceTuru.Ad : "",
                    p.TanzimTarihi,
                    p.BaslangicTarihi,
                    p.PoliceSuresi,
                    Bitis = p.BaslangicTarihi.AddMonths(p.PoliceSuresi),
                    p.Fiyat
                })
                .Where(x => x.Bitis <= cutoff)
                .OrderBy(x => x.Bitis)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("PoliceNo;Kisi;TC;Telefon;Sirket;Tur;Tanzim;Baslangic;SureAy;Bitis;Fiyat;Durum;KalanGun");
            foreach (var x in list)
            {
                var kalan = (x.Bitis - today).Days;
                var durum = x.Bitis < today ? "Süresi Doldu" : "Yaklaşıyor";
                sb.AppendLine($"{x.PoliceNo};{x.Kisi};{x.TcNo};{x.TelefonNo};{x.Sirket};{x.Tur};{x.TanzimTarihi:dd.MM.yyyy};{x.BaslangicTarihi:dd.MM.yyyy};{x.PoliceSuresi};{x.Bitis:dd.MM.yyyy};{x.Fiyat:0.##};{durum};{kalan}");
            }
            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"vade-raporu-{today:yyyyMMdd}.csv");
        }

        // Modal/harici kullanım için poliçe detay JSON (opsiyonel)
        // /Raporlar/PoliceDetay/5
        [HttpGet]
        public async Task<IActionResult> PoliceDetay(int id)
        {
            var today = DateTime.Today;

            var dto = await _ctx.Policeler
                .Include(p => p.SigortaSirketi)
                .Include(p => p.PoliceTuru)
                .Where(p => p.Id == id)
                .Select(p => new PoliceVadeDto
                {
                    Id = p.Id,
                    PoliceNo = p.PoliceNo ?? "",
                    Kisi = p.Kisi ?? "",
                    TcNo = p.TcNo,
                    TelefonNo = p.TelefonNo,
                    SigortaSirketi = p.SigortaSirketi != null ? p.SigortaSirketi.Ad : "",
                    PoliceTuru = p.PoliceTuru != null ? p.PoliceTuru.Ad : "",
                    Tanzim = p.TanzimTarihi,
                    Baslangic = p.BaslangicTarihi,
                    SureAy = p.PoliceSuresi,
                    Bitis = p.BaslangicTarihi.AddMonths(p.PoliceSuresi),
                    Fiyat = p.Fiyat
                })
                .FirstOrDefaultAsync();

            if (dto == null) return NotFound();

            dto.KalanGun = (dto.Bitis - today).Days;
            dto.Durum = dto.Bitis < today ? "Süresi Doldu" : "Yaklaşıyor";

            return Json(dto);
        }
    }
}
