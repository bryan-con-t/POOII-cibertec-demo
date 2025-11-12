using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POOII_cibertec_demo.Data;
using POOII_cibertec_demo.Models;

var builder = WebApplication.CreateBuilder(args);
// 1️⃣ Conexión a BD
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ Configuración de Identity (versión completa y compatible)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// 3️⃣ Agregar soporte MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 4️⃣ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 5️⃣ Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
