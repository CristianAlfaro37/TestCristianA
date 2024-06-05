using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using PruebaCristianAlfaro.Data;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(Options =>
    {
        Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


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

    app.Run();


}
catch (Exception ex)
{
    logger.Error(ex,"El programa ha parado por una excepcion");
}
finally
{
    NLog.LogManager.Shutdown();
}

