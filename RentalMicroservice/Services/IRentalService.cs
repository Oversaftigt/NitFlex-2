using RentalMicroservice.Models;

namespace RentalMicroservice.Services
{
    public interface IRentalService
    {
        void CreateRental(CreateRentalItem createRentalItem);
        List<RentalItem> GetAllRentalByUser(Guid userId);
        bool DoesUserHaveRentalForThisMovie(Guid movieId, Guid userId);
    }
}
