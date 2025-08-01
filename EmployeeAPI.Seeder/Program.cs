using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EmployeeAPI.Models;
using EmployeeAPI.Data;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment;
        config.Sources.Clear();
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);
    })
    .ConfigureServices((context, services) =>
    {
        var env = context.HostingEnvironment;
        var conStr = context.Configuration.GetConnectionString("DefaultConnection");

        if (env.IsProduction())
        {
            services.Configure<DBSettings>(context.Configuration.GetSection("DBSettings"));
            var ctxtPoolSize = int.TryParse(context.Configuration["ContextPoolSize"], out int tempCtxtPoolSize) ? tempCtxtPoolSize : 15;
            services.AddDbContext<AppDbContext>(optionsAction: (options) =>
            {
                var settings = context.Configuration.GetSection("DBSettings").Get<DBSettings>();
                if (settings != null)
                {
                    conStr = conStr?.Replace(settings.Server, Environment.GetEnvironmentVariable(settings.Server));
                    conStr = conStr?.Replace(settings.Port, Environment.GetEnvironmentVariable(settings.Port));
                    conStr = conStr?.Replace(settings.Database, Environment.GetEnvironmentVariable(settings.Database));
                    conStr = conStr?.Replace(settings.User, Environment.GetEnvironmentVariable(settings.User));
                    conStr = conStr?.Replace(settings.Password, Environment.GetEnvironmentVariable(settings.Password));
                }
                options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
            });
        }
        else
        {
            services.AddDbContext<AppDbContext>(options => options.UseMySql(conStr, ServerVersion.AutoDetect(conStr)));
        }
    })
    .Build();

using var scope = builder.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
DbInitializer.Initialize(db);
