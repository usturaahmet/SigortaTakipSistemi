using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Models;
using SigortaTakipSistemi.Data;

public class SigortaSirketiController : Controller
{
    private readonly ApplicationDbContext _context;

    public SigortaSirketiController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _context.SigortaSirketleri.ToListAsync();
        return View(list);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SigortaSirketi model)
    {
        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var sirket = await _context.SigortaSirketleri.FindAsync(id);
        if (sirket == null) return NotFound();
        return View(sirket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SigortaSirketi model)
    {
        if (id != model.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var sirket = await _context.SigortaSirketleri.FindAsync(id);
        if (sirket == null) return NotFound();
        return View(sirket);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sirket = await _context.SigortaSirketleri.FindAsync(id);
        _context.SigortaSirketleri.Remove(sirket);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
