using Microsoft.EntityFrameworkCore;
using MusicApp.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISongRepo, DbSongRepo>();
builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DBConn")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "songList",
//    pattern: "{controller=Home}/{action=SongList}/{id}");

app.Run();
