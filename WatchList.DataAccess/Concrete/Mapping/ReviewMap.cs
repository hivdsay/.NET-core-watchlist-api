using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Mapping;

public class ReviewMap : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Comment).HasMaxLength(500).IsRequired(false);
        builder.Property(m => m.UserRating).IsRequired();
        builder.Property(m => m.ReviewTitle).HasMaxLength(200);
        builder.Property(m => m.ReviewDate).IsRequired();
        builder.Property(m => m.IsRecommended).IsRequired();
        builder.Property(m => m.HelpfulCount).HasDefaultValue(0).IsRequired();
        builder.Property(m => m.IsSpoiler).IsRequired();
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews) 
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.Movie)
            .WithMany(m => m.Reviews) 
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}