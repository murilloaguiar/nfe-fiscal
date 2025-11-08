using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NfeFiscal.Models;

namespace NfeFiscal.Context.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable("Item");
        entity.Property(e => e.Description).HasColumnName("Descricao");
        entity.Property(e => e.Value).HasColumnName("Valor");
        entity.Property(e => e.InvoiceId).HasColumnName("NotaFiscalId");

        entity.HasOne(e => e.Invoice).WithMany(i => i.Items);

    }
}
