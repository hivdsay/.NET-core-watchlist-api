namespace WatchList.Core.Tools.Concrete.Dto.UserWatchList.Response;

public class UserWatchListResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string Status { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime? WatchedDate { get; set; }
    public int PersonalRating { get; set; }
    public string Notes { get; set; } = string.Empty;
}