using WatchList.Entities.Concrete.Base;

namespace WatchList.Entities.Concrete;

public class UserWatchList : BaseEntity
{
    //Diğer tablolarla bağlantılı
    public int UserId { get; set; } //Hangi kullancının listesi
    public int MovieId { get; set; } //Hangi film veya movie
    
    public string Status { get; set; } // İzleme durumu-- izledi mi, izleyecek mi, izleniyor mu
    public DateTime AddedDate { get; set; } = DateTime.UtcNow; //Listeye ne zaman eklendiği
    public DateTime? WatchedDate { get; set; } = DateTime.UtcNow; // Ne zaman izlendi? Film henüz izlenmediyse null
    public int PersonalRating { get; set; } // Kulanıcının kişisel puanı, Puan vermediyse null 
    public string Notes { get; set; } = string.Empty;// Kullanıcı notları, opsiyonel, not yazmazsa null
    
    //Bu watchlist kaydı hangi kullanıcıya ait? (Many-to-One ilişki)
    public User User { get; set; }
    
    //Bu watchlist kaydı hangi film/dizi hakkında? (Many-to-One ilişki)
    public Movie Movie { get; set; }
    
}