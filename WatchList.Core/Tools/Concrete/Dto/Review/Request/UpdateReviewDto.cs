namespace WatchList.Core.Tools.Concrete.Dto.Review.Request;

public class UpdateReviewDto
{
    public int Id { get; set; } // Güncellenecek review Id
    public int UserRating { get; set; } // Yeni puan
    public string Comment { get; set; } = string.Empty; // Yeni yorum
    public string ReviewTitle { get; set; } = string.Empty; // Yeni başlık
    public bool IsRecommended { get; set; } // Tavsiye durumu
    public bool IsSpoiler { get; set; } // Spoiler durumu
}