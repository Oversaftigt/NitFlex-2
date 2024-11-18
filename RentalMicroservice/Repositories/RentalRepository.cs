using RentalMicroservice.Models;

namespace RentalMicroservice.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        void IRentalRepository.CreateRental(CreateRentalItem createRentalItem)
        {
            throw new NotImplementedException();
        }

        List<RentalItem> IRentalRepository.GetAllRentalByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        bool IRentalRepository.DoesUserHaveRentalForThisMovie(Guid movieId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
