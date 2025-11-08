using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NfeFiscal.Models;

namespace NfeFiscal.Context.Configuration;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> entity)
    {
        entity.ToTable("NotaFiscal");
        entity.Property(e => e.Issuer).HasColumnName("Emissor");
        entity.Property(e => e.IssueDate).HasColumnName("DataEmissao");

        entity.HasMany(e => e.Items).WithOne(i => i.Invoice);
            
    }
}
