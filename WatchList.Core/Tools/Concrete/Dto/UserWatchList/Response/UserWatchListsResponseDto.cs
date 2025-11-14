namespace WatchList.Core.Tools.Concrete.Dto.UserWatchList.Response;

public class UserWatchListsResponseDto
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime AddedDate { get; set; }
    public int PersonalRating { get; set; }

    // Liste için genelde isim/email/title lazım
    public string UserEmail { get; set; }
    public string MovieTitle { get; set; }
}