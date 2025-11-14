namespace WatchList.Core.Tools.Concrete.Dto.Review.Request;

public class CreateReviewDto
{
    public int UserId {get; set;} //Yorumu kim yazdı
    public int MovieId {get; set;} // Hangi film veya movie
    public int UserRating {get; set;} // Kullanıcın verdiği puanı
    public string Comment { get; set; } = string.Empty;  // Yorum metni
    public string ReviewTitle { get; set; } = string.Empty; // Yorum başlığı, girmezse null
    public bool IsRecommended { get; set; } // Tavsiye durumu
    public bool IsSpoiler { get; set; } // Spoiler durumu
}