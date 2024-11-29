using RentalMicroservice.Models;

namespace RentalMicroservice.Repositories
{
    public interface IRentalRepository
    {
        void CreateRental(CreateRentalItem createRentalItem);
        List<RentalItem> GetAllRentalByUser(Guid userId);
        bool DoesUserHaveValidRentalForThisMovie(RentalValidationRequest rentalValidation);
    }
}
