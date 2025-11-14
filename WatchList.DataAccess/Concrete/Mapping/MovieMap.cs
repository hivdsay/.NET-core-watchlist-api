using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.Mapping;
//Nesne-ilişkisel eşleme (ORM) olarak da bilinen model eşleme, veritabanı tablolarındaki verileri bir uygulamadaki karşılık gelen model nesnelerine eşlemek için kullanılan bir tekniktir.
public class MovieMap : IEntityTypeConfiguration<Movie>
{
    
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Year).IsRequired();
        builder.Property(m => m.Title).HasMaxLength(200).IsRequired();
        builder.Property(m => m.Duration).IsRequired();
        builder.Property(m => m.Genre).HasMaxLength(50).IsRequired();
        builder.Property(m => m.IMDbRating).IsRequired();
        builder.Property(m => m.PosterUrl).HasMaxLength(500);
        builder.Property(m => m.Director).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Actors).HasMaxLength(500);
        builder.Property(m => m.Description).HasMaxLength(500); 
        
        builder.HasMany(m => m.Reviews)
            .WithOne(r => r.Movie)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(m => m.WatchLists)
            .WithOne(uw => uw.Movie)
            .HasForeignKey(uw => uw.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}