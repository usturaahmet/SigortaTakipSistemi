using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using SigortaTakipSistemi.Models;
using YourNamespace.Data;
using Microsoft.EntityFrameworkCore;

public class PoliceController : Controller
{
    private readonly ApplicationDbContext _context;

    public PoliceController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var policeler = await _context.Policeler
            .Include(p => p.SigortaSirketi)
            .Include(p => p.PoliceTuru)
            .Include(p => p.Personel)
            .ToListAsync();

        return View(policeler);
    }

    // GET: /Police/Create
    public IActionResult Create()
    {
        ViewBag.Sirketler = new SelectList(_context.SigortaSirketleri, "Id", "Ad");
        ViewBag.Turler = new SelectList(_context.PoliceTurleri, "Id", "Ad");
        ViewBag.Personeller = new SelectList(_context.Kullanicilar, "Id", "IsimSoyisim");

        return View();
    }

    // POST: /Police/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Police model)
    {
        // ViewBag'leri tekrar doldur
        ViewBag.Sirketler = new SelectList(_context.SigortaSirketleri, "Id", "Ad", model.SigortaSirketiId);
        ViewBag.Turler = new SelectList(_context.PoliceTurleri, "Id", "Ad", model.PoliceTuruId);
        ViewBag.Personeller = new SelectList(_context.Kullanicilar, "Id", "IsimSoyisim", model.PersonelId);

        if (ModelState.IsValid)
        {
            _context.Policeler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
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
        _context.Policeler.Remove(police);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
