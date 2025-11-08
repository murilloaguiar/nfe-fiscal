using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NfeFiscal.Models;
using NfeFiscal.Models.Enums;


namespace NfeFiscal.Context.Configuration;

public class ExportConfiguration : IEntityTypeConfiguration<Export>
{
    public void Configure(EntityTypeBuilder<Export> entity)
    {

        entity
            .Property(e => e.Format)
            .HasConversion(
                fromObj => fromObj.ToString(),
                fromDb => Enum.Parse<ExportFormat>(fromDb)
            );

    }
}
