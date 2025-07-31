using EmployeeLibrary.Mdoels;
using EmployeeLibrary.Repositories;
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
    var ctxtPoolSize = int.TryParse(builder.Configuration["ContextPoolSize"], out int tempCtxtPoolSize) ? tempCtxtPoolSize : 15;
    builder.Services.AddDbContextPool<AppDbContext>(poolSize: ctxtPoolSize, optionsAction: (serviceProvider, options) =>
    {
        var settings = serviceProvider.GetRequiredService<DBSettings>();
        conStr = conStr?.Replace(settings.Server, Environment.GetEnvironmentVariable(settings.Server));
        conStr = conStr?.Replace(settings.Port, Environment.GetEnvironmentVariable(settings.Port));
        conStr = conStr?.Replace(settings.Database, Environment.GetEnvironmentVariable(settings.Database));
        conStr = conStr?.Replace(settings.User, Environment.GetEnvironmentVariable(settings.User));
        conStr = conStr?.Replace(settings.Password, Environment.GetEnvironmentVariable(settings.Password));
        conStr = conStr?.Replace(settings.PoolSize, Environment.GetEnvironmentVariable(settings.PoolSize));
        var podName = Environment.GetEnvironmentVariable("POD_Name") ?? "unknown-pod";
        conStr = conStr?.Replace("POD_Name", Environment.GetEnvironmentVariable("POD_Name"));
        options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
    });
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(conStr, ServerVersion.AutoDetect(conStr)));
}

var app = builder.Build();

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