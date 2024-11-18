namespace MovieMicroservice.Models
{
    public class CreateMovieItem
    {
        public string MovieName { get; set; } = string.Empty;
        public string MovieDescription { get; set; } = string.Empty;
        public string MovieGenre { get; set; } = string.Empty;
        public int MovieDurationInMinutes { get; set; }
        public double MovieRentalPrice { get; set; }
    }
}
