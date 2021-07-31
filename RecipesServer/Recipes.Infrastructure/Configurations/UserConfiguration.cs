using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasIndex(p => p.Login).IsUnique();
            
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
        }
    }
}