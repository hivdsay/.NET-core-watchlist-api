using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Mapping;

public class UserWatchListMap : IEntityTypeConfiguration<UserWatchList>
{
    public void Configure(EntityTypeBuilder<UserWatchList> builder)
    {
        builder.HasKey(uw => uw.Id);

        builder.Property(uw => uw.Status).HasMaxLength(50).IsRequired();

        builder.Property(uw => uw.AddedDate).IsRequired();

        builder.Property(uw => uw.WatchedDate).IsRequired();

        builder.Property(uw => uw.PersonalRating).IsRequired();
        builder.Property(uw => uw.Notes).HasMaxLength(500);
        
        // User ile ilişki (Many-to-One)
        builder.HasOne(uw => uw.User)
            .WithMany(u => u.WatchLists)
            .HasForeignKey(uw => uw.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Movie ile ilişki (Many-to-One)
        builder.HasOne(uw => uw.Movie)
            .WithMany(m => m.WatchLists)
            .HasForeignKey(uw => uw.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}