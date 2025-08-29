using System.Reflection;
using EnviroSense.Application;
using EnviroSense.Plugins.PostgresRepositories;
using EnviroSense.Plugins.RazorEmailing;
using EnviroSense.Plugins.SMTPClient;
using EnviroSense.Web;
using EnviroSense.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Adjust configuration
builder.Configuration.AddJsonFile("/etc/secrets/appsettings.Production.json", optional: true);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddPostgresRepositories(builder.Configuration.GetConnectionString("Default") ??
                                         throw new Exception("Missing connection string"));
builder.Services.AddDistributedPostgresCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Default");
    options.SchemaName = "public";
    options.TableName = "cache";
    options.CreateIfNotExists = true;
    options.UseWAL = false;
});
builder.Services.AddSMTPClient(builder.Configuration.GetRequiredSection("EmailSettings") ??
                               throw new Exception("Missing email settings"));
builder.Services.AddRazorEmailing();

builder.Services.AddWebServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MustMigrate();
app.UseRouting();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
