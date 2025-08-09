using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigortaTakipSistemi.Data;
using SigortaTakipSistemi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SigortaTakipSistemi.Controllers
{
    public class PoliceTuruController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public PoliceTuruController(ApplicationDbContext ctx) => _ctx = ctx;

        // LISTE
        public async Task<IActionResult> Index()
        {
            var list = await _ctx.PoliceTurleri
                                 .AsNoTracking()
                                 .OrderBy(x => x.Ad)
                                 .ToListAsync();
            return View(list);
        }

        // CREATE
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PoliceTuru m)
        {
            if (!ModelState.IsValid) return View(m);
            _ctx.PoliceTurleri.Add(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var m = await _ctx.PoliceTurleri.FindAsync(id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PoliceTuru m)
        {
            if (!ModelState.IsValid) return View(m);
            _ctx.Update(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _ctx.PoliceTurleri.FindAsync(id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _ctx.PoliceTurleri.FindAsync(id);
            if (m != null)
            {
                _ctx.PoliceTurleri.Remove(m);
                await _ctx.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var m = await _ctx.PoliceTurleri
                              .AsNoTracking()
                              .FirstOrDefaultAsync(x => x.Id == id);
            if (m == null) return NotFound();
            return View(m);
        }
    }
}
