namespace WatchList.Core.Tools.Concrete.Dto.Review.Response;

public class ResponseReviewDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int UserRating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string ReviewTitle { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    public bool IsRecommended { get; set; }
    public int HelpfulCount { get; set; }
    public bool IsSpoiler { get; set; }
}