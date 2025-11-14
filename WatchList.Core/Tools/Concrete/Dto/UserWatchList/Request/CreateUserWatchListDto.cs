namespace WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;

public class CreateUserWatchListDto
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string Status { get; set; } // "To Watch", "Watching", "Watched"
    public int PersonalRating { get; set; } // opsiyonel
    public string Notes { get; set; } = string.Empty; // opsiyonel
}