using Microsoft.EntityFrameworkCore;
using NfeFiscal.Models;

namespace NfeFiscal.Context;

public class LogContext : DbContext
{
    public LogContext(DbContextOptions<LogContext> options) : base(options)
    {

    }
   
    public DbSet<EventLog> EventLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EventLog>()
            .ToTable("Logs");

    }
}
