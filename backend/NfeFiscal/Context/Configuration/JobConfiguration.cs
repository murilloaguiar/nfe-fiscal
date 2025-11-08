using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NfeFiscal.Models;
using NfeFiscal.Models.Enums;


namespace NfeFiscal.Context.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> entity)
    {
        entity
            .Property(e => e.Status)
            .HasConversion(
                fromObj => fromObj.ToString(),
                fromDb => Enum.Parse<JobStatus>(fromDb)
            );

        entity
            .Property(e => e.Format)
            .HasConversion(
                fromObj => fromObj.ToString(),
                fromDb => Enum.Parse<ExportFormat>(fromDb)
            );

    }
}
