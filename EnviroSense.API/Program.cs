using EnviroSense.API;
using EnviroSense.API.Filters;
using EnviroSense.Application;
using EnviroSense.Plugins.PostgresRepositories;
using EnviroSense.Plugins.SMTPClient;

var builder = WebApplication.CreateBuilder(args);

// Adjust configuration
builder.Configuration.AddJsonFile("/etc/secrets/appsettings.Production.json", optional: true);


// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddPostgresRepositories(builder.Configuration.GetConnectionString("Default") ?? throw new Exception("Missing connection string"));
builder.Services.AddDistributedPostgresCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Default");
    options.SchemaName = "public";
    options.TableName = "cache";
    options.CreateIfNotExists = true;
    options.UseWAL = false;
});
builder.Services.AddSMTPClient(builder.Configuration.GetRequiredSection("EmailSettings") ?? throw new Exception("Missing email settings"));
builder.Services.AddAPIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MustMigrate();
app.UseRouting();
app.MapControllers();

app.Run();
