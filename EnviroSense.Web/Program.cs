using EnviroSense.Application;
using EnviroSense.Plugins.PostgresRepositories;
using EnviroSense.Plugins.SMTPClient;
using EnviroSense.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

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

// Add app related services.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
var mvcBuilder = builder.Services.AddControllersWithViews(opts =>
{
    opts.Filters.Add<AccessTrackingFilter>();
});

#if DEBUG
mvcBuilder.AddRazorRuntimeCompilation();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MustMigrate();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
