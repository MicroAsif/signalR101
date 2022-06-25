using Microsoft.EntityFrameworkCore;
using signalR101;
using signalR101.Interfaces;
using signalR101.Models;
using signalR101.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddSignalR();

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
app.UseSession();
app.UseAuthorization();


app.UseEndpoints(endpoints => { endpoints.MapHub<SignalServer>("/signalServer"); });

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");


app.Run();