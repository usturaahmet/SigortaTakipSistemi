using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Data;
using SigortaTakipSistemi.Models;

namespace SigortaTakipSistemi.Controllers
{
    public class PoliceTuruSirketController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public PoliceTuruSirketController(ApplicationDbContext ctx) => _ctx = ctx;

        // İstersen turId ile filtreli liste
        public IActionResult Index(int? turId)
        {
            var q = _ctx.PoliceTuruSirketler
                .Include(x => x.SigortaSirketi)
                .Include(x => x.PoliceTuru)
                .AsQueryable();

            if (turId.HasValue)
                q = q.Where(x => x.PoliceTuruId == turId);

            var list = q.OrderBy(x => x.SigortaSirketi.Ad)
                        .ThenBy(x => x.PoliceTuru.Ad)
                        .ToList();

            return View(list); // View'i aşağıda verdim
        }

        // Poliçe Ekle ekranının çağıracağı endpoint
        // 1) Şirket+Tür özel oranı varsa onu döndürür
        // 2) Yoksa PoliceTuru (varsayılan) Prim/AcentaPrimi'ni döndürür
        [HttpGet]
        public IActionResult GetRates(int sirketId, int turId)
        {
            var r = _ctx.PoliceTuruSirketler
                .FirstOrDefault(x => x.SigortaSirketiId == sirketId &&
                                     x.PoliceTuruId == turId &&
                                     x.Aktif);
            if (r != null)
                return Json(new { prim = r.PrimYuzde, acenta = r.AcentaPrimiYuzde });

            var t = _ctx.PoliceTurleri
                .Where(x => x.Id == turId)
                .Select(x => new { prim = x.Prim, acenta = x.AcentaPrimi })
                .FirstOrDefault();

            return Json(t); // t null olabilir → ekranda “oran bulunamadı” uyarısı göster
        }

        // (Opsiyonel) Poliçe Ekle içinden modal ile hızlı ekleme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInline(int sigortaSirketiId, int policeTuruId,
                                          decimal primYuzde, decimal acentaPrimiYuzde)
        {
            if (!_ctx.SigortaSirketleri.Any(x => x.Id == sigortaSirketiId) ||
                !_ctx.PoliceTurleri.Any(x => x.Id == policeTuruId))
                return Json(new { ok = false, message = "Geçersiz şirket/tür seçimi." });

            var exists = _ctx.PoliceTuruSirketler
                .Any(x => x.SigortaSirketiId == sigortaSirketiId &&
                          x.PoliceTuruId == policeTuruId);
            if (exists)
                return Json(new { ok = false, message = "Bu eşleşme zaten kayıtlı." });

            _ctx.PoliceTuruSirketler.Add(new PoliceTuruSirket
            {
                SigortaSirketiId = sigortaSirketiId,
                PoliceTuruId = policeTuruId,
                PrimYuzde = primYuzde,
                AcentaPrimiYuzde = acentaPrimiYuzde,
                Aktif = true
            });
            _ctx.SaveChanges();
            return Json(new { ok = true });
        }
    }
}
