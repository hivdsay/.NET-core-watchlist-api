using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Mapping;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Tablo adı
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RoleName)
            .HasConversion<int>() 
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(200);

        // Relationships
        builder.HasMany(x => x.UserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(
            new Role 
            { 
                Id = 1, 
                RoleName = "Admin", 
                Description = "System Administrator",
                CreatedDate = DateTime.Now,
                IsActive = true
            },
            new Role 
            { 
                Id = 2, 
                RoleName = "User", 
                Description = "Regular User",
                CreatedDate = DateTime.Now,
                IsActive = true
            }
        );
    }
}