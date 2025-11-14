namespace WatchList.Core.Tools.Concrete.Dto.Movie.Response;

public class MovieListResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public decimal IMDbRating { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
}