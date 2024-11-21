namespace MovieMicroservice.Models.DTOs
{
    public class RentalValidationRequest
    {
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
    }
}
