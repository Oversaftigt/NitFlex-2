using RentalMicroservice.Models;
using RentalMicroservice.Repositories;

namespace RentalMicroservice.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        void IRentalService.CreateRental(CreateRentalItem createRentalItem)
        {
            _rentalRepository.CreateRental(createRentalItem);
        }

        List<RentalItem> IRentalService.GetAllRentalByUser(Guid userId)
        {
            return _rentalRepository.GetAllRentalByUser(userId);
        }

        bool IRentalService.DoesUserHaveRentalForThisMovie(Guid movieId, Guid userId)
        {
            return _rentalRepository.DoesUserHaveRentalForThisMovie(movieId, userId);
        }
    }
}
