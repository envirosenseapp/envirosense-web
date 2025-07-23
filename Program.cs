using EnviroSense.Web;
using EnviroSense.Web.Filters;
using EnviroSense.Web.Migrations;
using EnviroSense.Web.Repositories;
using EnviroSense.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAccessRepository, AccessRepository>();
builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IDeciveRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<Migrator>();
builder.Services.AddControllersWithViews(opts =>
{
    opts.Filters.Add<AccessTrackingFilter>();

});

var app = builder.Build();

// Run migrations
using (var serviceScope = app.Services.CreateScope())
{
    var migrator = serviceScope.ServiceProvider.GetService<Migrator>();
    migrator.MigrateDatabase();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
