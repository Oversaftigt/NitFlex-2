namespace BlazorNitflex.Models
{
    public class CreateRentalItem
    {
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
    }
}
