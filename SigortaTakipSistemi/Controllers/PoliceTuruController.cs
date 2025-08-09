using Microsoft.AspNetCore.Mvc;
using SigortaTakipSistemi.Models;
using SigortaTakipSistemi.Data;
using Microsoft.EntityFrameworkCore;

public class PoliceTuruController : Controller
{
    private readonly ApplicationDbContext _context;

    public PoliceTuruController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _context.PoliceTurleri.ToListAsync();
        return View(list);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PoliceTuru model)
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
        var item = await _context.PoliceTurleri.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PoliceTuru model)
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
        var item = await _context.PoliceTurleri.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.PoliceTurleri.FindAsync(id);
        _context.PoliceTurleri.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
