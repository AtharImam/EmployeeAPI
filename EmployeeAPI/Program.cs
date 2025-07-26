using EmployeeAPI.Data;
using EmployeeAPI.Helpers;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
var conStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("DBSettings"));

builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DBSettings>>().Value);

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
    {
        var settings = serviceProvider.GetRequiredService<DBSettings>();
        conStr = conStr?.Replace(settings.Server, Environment.GetEnvironmentVariable(settings.Server));
        conStr = conStr?.Replace(settings.Port, Environment.GetEnvironmentVariable(settings.Port));
        conStr = conStr?.Replace(settings.Database, Environment.GetEnvironmentVariable(settings.Database));
        conStr = conStr?.Replace(settings.User, Environment.GetEnvironmentVariable(settings.User));
        conStr = conStr?.Replace(settings.Password, Environment.GetEnvironmentVariable(settings.Password));
        conStr = conStr?.Replace(settings.Password, Environment.GetEnvironmentVariable(settings.Password));
        options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
    });
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(conStr, ServerVersion.AutoDetect(conStr)));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "OK");

app.Run();