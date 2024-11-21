namespace RentalMicroservice.Models
{
    public class RentalValidationRequest
    {
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
    }
}
