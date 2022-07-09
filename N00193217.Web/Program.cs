using Microsoft.EntityFrameworkCore;
using N00193217.Web.DB;
using N00193217.Web.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbEntities>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

builder.Services.AddTransient<ICuentaRepositorio, CuentaRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cuenta}/{action=Index}/{id?}");

app.Run();
