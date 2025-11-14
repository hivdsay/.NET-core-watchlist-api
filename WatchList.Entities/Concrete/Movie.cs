using WatchList.Entities.Concrete.Base;

namespace WatchList.Entities.Concrete;

public class Movie : BaseEntity
{
    public string Title { get; set; } //Film adı
    public int Year { get; set; } //Çıkış yılı
    public int Duration { get; set; } //Süre
    public string Genre { get; set; } //Tür
    
    public decimal IMDbRating { get; set; } //IMDb'den gelen puan
    public string PosterUrl { get; set; } = string.Empty; // Poster resmi linki
    public string Director { get; set; } //Yönetmen
    public string Actors { get; set; } = string.Empty; //Oyuncular
    public string Description { get; set; } = string.Empty;//Film açıklması
    
    //Bu filmi listesine ekleyen tüm kullanıcılar, (Many-to-Many ilişki)
    public List<UserWatchList> WatchLists { get; set; } = new List<UserWatchList>(); //NullReferenceException fırlatmaz.
    
    //Bu film hakkında yazılmış tüm yorumlar
    public List<Review> Reviews { get; set; } = new List<Review>();
}