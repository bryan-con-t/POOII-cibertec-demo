using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POOII_cibertec_demo.Data;
using POOII_cibertec_demo.Models;
using POOII_cibertec_demo.Repositories;

namespace POOII_cibertec_demo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(
            string nombre = null,
            decimal? precioMin = null,
            int? cantidadMin = null,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null,
            bool? isCompleted = null,
            int page = 1,
            int pageSize = 5)
        {
            // Crear repo usando la cadena de conexión del contexto
            var connectionString = _context.Database.GetDbConnection().ConnectionString;
            var repo = new ProductRepository(connectionString);

            //Llamada al repo (async)
            var (items, total) = await repo.FiltrarPaginadoAsync(
                nombre,
                precioMin,
                cantidadMin,
                fechaDesde,
                fechaHasta,
                isCompleted,
                page,
                pageSize);

            // Preparar ViewBag para que la vista mantenga los valores en los inputs
            ViewBag.Nombre = nombre ?? "";
            ViewBag.PrecioMin = precioMin?.ToString("0.##") ?? "";
            ViewBag.CantidadMin = cantidadMin?.ToString() ?? "";
            // Paginación
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = total;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)total / pageSize);

            return View(items);
            // return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Producto eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
