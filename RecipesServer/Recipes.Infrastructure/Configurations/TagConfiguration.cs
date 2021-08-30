using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tag");

            builder.HasKey(x => x.Value);
            builder.Property(x => x.Value).IsRequired();
            builder.HasIndex(x => x.Value).IsUnique();
        }
    }
}