using Microsoft.EntityFrameworkCore;
using WatchList.DataAccess.Concrete.Mapping;
using WatchList.Entities.Concrete;

namespace WatchList.DataAccess.Concrete.AppContext;

//İçinde hangi tabloların olacağını, ilişkilerin nasıl kurulacağını burada tanımlarız.
//DbContext, EF Core’un veritabanıyla iletişim kurduğu ana sınıftır.
public class WatchListDbContext : DbContext
{
    public WatchListDbContext(DbContextOptions<WatchListDbContext> options) : base(options)
    { //EF Core’a hangi veritabanını, bağlantı stringini ve diğer ayarları kullanacağını söyler.
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new ReviewMap());
        modelBuilder.ApplyConfiguration(new UserWatchListMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new UserRoleMap());
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserWatchList> UserWatchLists { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    

 
}