namespace BlazorNitflex.Models
{
    public class MovieItem
    {
        public Guid Id { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public string MovieDescription { get; set; } = string.Empty;
        public string MovieGenre { get; set; } = string.Empty;
        public int MovieDurationInMinutes { get; set; }
        public double MovieRentalPrice { get; set; }
    }
}
