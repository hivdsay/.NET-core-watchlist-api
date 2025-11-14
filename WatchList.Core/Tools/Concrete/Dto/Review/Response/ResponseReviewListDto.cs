namespace WatchList.Core.Tools.Concrete.Dto.Review.Response;

public class ResponseReviewListDto
{
    public int Id { get; set; }
    public int UserRating { get; set; }
    public string ReviewTitle { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    public bool IsRecommended { get; set; }
}