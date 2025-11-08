using Microsoft.EntityFrameworkCore;
using NfeFiscal.Context;
using NfeFiscal.Mappers;
using NfeFiscal.Services;
using NfeFiscal.UnitOfWork;
using NfeFiscal.Workers;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ExportWorker>();

string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING") ?? "";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

builder.Services.AddDbContext<NfeContext>(options =>
        options.UseMySql(connectionString, serverVersion)
    );

builder.Services.AddDbContext<LogContext>(options =>
        options.UseSqlite(@"Data Source=log.db"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILogUnitOfWork, LogUnitOfWork>();
builder.Services.AddTransient(typeof(InvoiceMapper));
builder.Services.AddTransient(typeof(ItemMapper));
builder.Services.AddScoped<IExportService, ExportService>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.SQLite(
        sqliteDbPath: Environment.CurrentDirectory + @"/log.db",
        tableName: "Logs",
        storeTimestampInUtc: true,
        batchSize: 1)
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
