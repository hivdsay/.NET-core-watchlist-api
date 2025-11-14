using WatchList.Entities.Concrete.Base;

namespace WatchList.Entities.Concrete;

public class User : BaseEntity
{
    public string FirstName { get; set; }
   
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    //Bu kullanıcının izleme listeleri, bir kullanıcının birden fazla film listesi olabilir
    public List<UserWatchList> WatchLists { get; set; } = new List<UserWatchList>();
    
    //Bu kullanıcının yazdığı yorumlar, bir kullanıcı birden fazla film yorumlayabilir
    public List<Review> Reviews { get; set; } = new List<Review>();
    
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
}