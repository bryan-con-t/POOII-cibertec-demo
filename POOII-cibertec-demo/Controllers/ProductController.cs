using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POOII_cibertec_demo.Application.Products.Commands;
using POOII_cibertec_demo.Application.Products.Services;
using POOII_cibertec_demo.Data;
using POOII_cibertec_demo.Infrastructure.Files;
using POOII_cibertec_demo.Infrastructure.Persistence;
using POOII_cibertec_demo.Models;
using POOII_cibertec_demo.Models.ViewModels;
using POOII_cibertec_demo.Repositories;

namespace POOII_cibertec_demo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index(
            string nombre = null,
            decimal? precioMin = null,
            int? cantidadMin = null,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null,
            bool? isCompleted = null,
            int? page = null,
            int? pageSize = null)
        {
            // Recuperar valores de la sesión si no se proporcionan
            nombre ??= HttpContext.Session.GetString("Nombre");
            if (precioMin == null && decimal.TryParse(HttpContext.Session.GetString("PrecioMin"), out var pMin))
                precioMin = pMin;
            if (cantidadMin == null && int.TryParse(HttpContext.Session.GetString("CantidadMin"), out var cMin))
                cantidadMin = cMin;
            if (isCompleted == null && bool.TryParse(HttpContext.Session.GetString("IsCompleted"), out var completed))
                isCompleted = completed;
            page ??= HttpContext.Session.GetInt32("Page") ?? 1;
            pageSize ??= HttpContext.Session.GetInt32("PageSize") ?? 5;

            // Crear repo usando la cadena de conexión del contexto
            var connectionString = _context.Database.GetDbConnection().ConnectionString;
            var repo = new POOII_cibertec_demo.Repositories.ProductRepository(connectionString);

            //Llamada al repo (async)
            var (items, total) = await repo.FiltrarPaginadoAsync(
                nombre,
                precioMin,
                cantidadMin,
                fechaDesde,
                fechaHasta,
                isCompleted,
                page.Value,
                pageSize.Value);

            // Guardar los datos en la sesión para usarlos en futuras solicitudes
            HttpContext.Session.SetString("Nombre", nombre ?? "");
            HttpContext.Session.SetString("PrecioMin", precioMin?.ToString() ?? "");
            HttpContext.Session.SetString("CantidadMin", cantidadMin?.ToString() ?? "");
            HttpContext.Session.SetString("IsCompleted", isCompleted?.ToString() ?? "");
            HttpContext.Session.SetInt32("Page", page.Value);
            HttpContext.Session.SetInt32("PageSize", pageSize.Value);

            // Preparar ViewBag para que la vista mantenga los valores en los inputs
            ViewBag.Nombre = nombre ?? "";
            ViewBag.PrecioMin = precioMin?.ToString("0.##") ?? "";
            ViewBag.CantidadMin = cantidadMin?.ToString() ?? "";

            // Paginación
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = total;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)total / pageSize.Value);

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
        public async Task<IActionResult> Edit(int id)
        {
            var repo = new POOII_cibertec_demo.Infrastructure.Persistence.ProductRepository(_context);
            var product = await repo.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            var vm = new ProductEditViewModel
            {
                Id = product.Id,
                nombre = product.nombre,
                precio = product.precio,
                cantidad = product.cantidad,
                isCompleted = product.isCompleted,
                imagenPath = product.imagenPath
            };

            return View(vm);
        }


        // POST: Products/Edit/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCommand command)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                command.UpdateProduct(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditViewModel vm, IFormFile imagen)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(vm);
            }

            var imageStorage = new ImageStorage(_env);
            var imagePath = await imageStorage.SaveAsync(imagen);

            var command = new UpdateProductCommand
            {
                Id = vm.Id,
                nombre = vm.nombre,
                precio = vm.precio,
                cantidad = vm.cantidad,
                isCompleted = vm.isCompleted,
                ImagenPath = imagePath
            };

            var repo = new Infrastructure.Persistence.ProductRepository(_context);
            var service = new ProductService(repo);

            await service.UpdateAsync(command);

            TempData["Success"] = "Producto actualizado correctamente.";
            return RedirectToAction(nameof(Index));
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

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Remove("Nombre");
            HttpContext.Session.Remove("PrecioMin");
            HttpContext.Session.Remove("CantidadMin");
            HttpContext.Session.Remove("IsCompleted");
            HttpContext.Session.Remove("Page");
            HttpContext.Session.Remove("PageSize");

            return RedirectToAction(nameof(Index));
        }
    }
}
