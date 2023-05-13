using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeatherApp.BLL.Implementation;
using WeatherApp.BLL.Interface;
using WeatherApp.DAL.Database;
using WeatherApp.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<WeatherAppDbContext>(opts =>
{
    var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];
    opts.UseSqlServer(defaultConn);
});

builder.Services.AddControllersWithViews();
// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<WeatherAppDbContext>>();
builder.Services.AddScoped<ICitiesServices, CitiesServices>();
builder.Services.AddAutoMapper( Assembly.Load("WeatherApp.BLL"));

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
    pattern: "{controller=Weather}/{action=HomePage}/{id?}");

app.Run();
