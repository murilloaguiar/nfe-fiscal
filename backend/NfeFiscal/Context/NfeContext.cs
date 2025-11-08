using Microsoft.EntityFrameworkCore;
using NfeFiscal.Models;

namespace NfeFiscal.Context;

public class NfeContext : DbContext
{
    public NfeContext(DbContextOptions<NfeContext> options) : base(options)
    {
        
    }

    public DbSet<Invoice> Invoices {  get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Export> Exports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NfeContext).Assembly);

    }
}
