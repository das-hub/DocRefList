using DocRefList.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocRefList.Data.Configuration
{
    public class DocumentInfoConfiguration : IEntityTypeConfiguration<DocumentInfo>
    {
        public void Configure(EntityTypeBuilder<DocumentInfo> builder)
        {
            builder
                .HasMany(d => d.Familiarizations)
                .WithOne(f => f.Document)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}