using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3K1DMidTest.Models;
using System.Threading.Tasks;

namespace _3K1DMidTest.Controllers
{
    public class PartController : Controller
    {
        private readonly CarDealerContext _context;

        public PartController(CarDealerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parts = await _context.Parts
                                      .Include(p => p.Supplier)
                                      .ToListAsync();
            return View(parts);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                                    .Include(p => p.Supplier)
                                    .FirstOrDefaultAsync(p => p.Id == id);

            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }
        // delete 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                                    .Include(p => p.Supplier)
                                    .FirstOrDefaultAsync(p => p.Id == id);

            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var supplierList = await _context.Suppliers
                                             .Select(s => new SelectListItem
                                             {
                                                 Value = s.Id.ToString(),
                                                 Text = s.Name
                                             })
                                             .ToListAsync();

            ViewBag.SupplierList = supplierList;
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }

            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Part part)
        {
            try
            {
                _context.Parts.Add(part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
                ModelState.AddModelError("", "Không thể lưu dữ liệu vào cơ sở dữ liệu. Vui lòng thử lại.");
            }

            var supplierList = await _context.Suppliers
                                             .Select(s => new SelectListItem
                                             {
                                                 Value = s.Id.ToString(),
                                                 Text = s.Name
                                             })
                                             .ToListAsync();

            ViewBag.SupplierList = supplierList;
            //return View(part);
            return RedirectToAction(nameof(Index));

        }
    }
}
