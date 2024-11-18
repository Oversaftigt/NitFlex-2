using RentalMicroservice.Models;

namespace RentalMicroservice.Services
{
    public class RentalService : IRentalService
    {
        void IRentalService.CreateRental(CreateRentalItem createRentalItem)
        {
            throw new NotImplementedException();
        }

        List<RentalItem> IRentalService.GetAllRentalByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        bool IRentalService.DoesUserHaveRentalForThisMovie(Guid movieId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
