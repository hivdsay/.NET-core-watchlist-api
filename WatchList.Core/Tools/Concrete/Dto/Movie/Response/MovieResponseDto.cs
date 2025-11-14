namespace WatchList.Core.Tools.Concrete.Dto.Movie.Response;

public class MovieResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; }
    public decimal IMDbRating { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public string Director { get; set; }
    public string Actors { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}