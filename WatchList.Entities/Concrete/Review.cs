using WatchList.Entities.Concrete.Base;

namespace WatchList.Entities.Concrete;

public class Review : BaseEntity
{
    // Diğer tablolarla bağlantılı, foreign keys
    public int UserId {get; set;} //Yorumu kim yazdı
    public int MovieId {get; set;} // Hangi film veya movie
   
    // Herkesin gördüğü comment içeriği
    public int UserRating {get; set;} // Kullanıcın verdiği puanı
    public string Comment { get; set; } = string.Empty;  // Yorum metni
    public string ReviewTitle { get; set; } = string.Empty; // Yorum başlığı, girmezse null
    
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow; //Ne zaman yorum yazıldı
    public bool IsRecommended { get; set; } // Tavsiye ediliyor mu
    public int HelpfulCount { get; set; } = 0; //"Kaç kişi yararlı dedi?" sayısı
    public bool IsSpoiler { get; set; } // Spoiler içeriyor mu
    
    public User User { get; set; } // Bu yorum hangi kullanıcıya ait? (Many-to-One ilişki)
    public Movie Movie { get; set; } // Bu yorum hangi film veya dizi hakkında? (Many-to-One ilişki)
    
}