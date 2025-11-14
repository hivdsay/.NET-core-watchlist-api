namespace WatchList.Core.Tools.Concrete.Dto.UserWatchList.Request;

public class UpdateUserWatchListDto
{
    public int Id { get; set; }
    public int UserWatchListId { get; set; }   // Güncellenecek kaydın Id’si
    public string Status { get; set; }
    public int PersonalRating { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime? WatchedDate { get; set; }
}