using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Data;
using SigortaTakipSistemi.Models;
using SigortaTakipSistemi.ViewModels;


public class PoliceController : Controller
{
    private readonly ApplicationDbContext _context;

    public PoliceController(ApplicationDbContext context)
    {
        _context = context;
    }

    private void FillDropdowns(PoliceViewModel vm)
    {
        vm.SigortaSirketleri = _context.SigortaSirketleri
            .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Ad })
            .ToList();

        vm.PoliceTurleri = _context.PoliceTurleri
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Ad })
            .ToList();

        vm.Personeller = _context.Kullanicilar
            .Select(k => new SelectListItem { Value = k.Id.ToString(), Text = k.IsimSoyisim })
            .ToList();
    }

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new PoliceViewModel();
        FillDropdowns(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PoliceViewModel vm)
    {
        // Sunucu tarafı asgari kontroller
        if (vm.BaslangicTarihi == default)
            ModelState.AddModelError(nameof(vm.BaslangicTarihi), "Başlangıç tarihi zorunlu.");

        if (vm.PoliceSuresi <= 0)
            ModelState.AddModelError(nameof(vm.PoliceSuresi), "Süre (ay) 1’den büyük olmalıdır.");

        if (!ModelState.IsValid)
        {
            // Hataları görmek istersen (geçici)
            ViewBag.ModelErrors = ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .Select(x => new { Field = x.Key, Err = x.Value!.Errors.Select(e => e.ErrorMessage) })
                .ToList();

            FillDropdowns(vm);
            return View(vm);
        }

        // BitisTarihi hesapla
        var bitis = vm.BaslangicTarihi.AddMonths(vm.PoliceSuresi);

        var entity = new Police
        {
            PoliceNo = vm.PoliceNo,
            Kisi = vm.Kisi,
            TcNo = vm.TcNo,
            TelefonNo = vm.TelefonNo,
            SigortaSirketiId = vm.SigortaSirketiId,
            PoliceTuruId = vm.PoliceTuruId,
            PersonelId = vm.PersonelId,
            TanzimTarihi = vm.TanzimTarihi,
            BaslangicTarihi = vm.BaslangicTarihi,
            PoliceSuresi = vm.PoliceSuresi,
            Fiyat = vm.Fiyat,
            BitisTarihi = bitis
        };

        _context.Policeler.Add(entity);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index()
    {
        var list = _context.Policeler
            .Include(p => p.SigortaSirketi)
            .Include(p => p.PoliceTuru)
            .Include(p => p.Personel)
            .OrderByDescending(p => p.Id)
            .ToList();

        return View(list);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var police = await _context.Policeler.FindAsync(id);
        if (police == null) return NotFound();

        ViewBag.Sirketler = new SelectList(_context.SigortaSirketleri, "Id", "Ad", police.SigortaSirketiId);
        ViewBag.Turler = new SelectList(_context.PoliceTurleri, "Id", "Ad", police.PoliceTuruId);
        ViewBag.Personeller = new SelectList(_context.Kullanicilar, "Id", "IsimSoyisim", police.PersonelId);

        return View(police);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Police model)
    {
        if (id != model.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Sirketler = new SelectList(_context.SigortaSirketleri, "Id", "Ad", model.SigortaSirketiId);
        ViewBag.Turler = new SelectList(_context.PoliceTurleri, "Id", "Ad", model.PoliceTuruId);
        ViewBag.Personeller = new SelectList(_context.Kullanicilar, "Id", "IsimSoyisim", model.PersonelId);

        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var police = await _context.Policeler
            .Include(p => p.SigortaSirketi)
            .Include(p => p.PoliceTuru)
            .Include(p => p.Personel)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (police == null) return NotFound();

        return View(police);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var police = await _context.Policeler.FindAsync(id);
        if (police is null) return NotFound();

        _context.Policeler.Remove(police);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
