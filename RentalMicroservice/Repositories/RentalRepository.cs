using RentalMicroservice.Data;
using RentalMicroservice.Models;

namespace RentalMicroservice.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalDbContext _context;

        public RentalRepository(RentalDbContext context)
        {
            _context = context;
        }

        void IRentalRepository.CreateRental(CreateRentalItem createRentalItem)
        {
            var rental = new RentalItem
            {
                MovieId = createRentalItem.MovieId,
                RentalEndDate = createRentalItem.RentalEndDate,
                RentalStartDate = createRentalItem.RentalStartDate,
                UserId = createRentalItem.UserId,
            };
            _context.Add(rental);
            _context.SaveChanges();
        }

        List<RentalItem> IRentalRepository.GetAllRentalByUser(Guid userId)
        {
            var rentals = _context.Rentals.Where(r => r.UserId == userId && r.RentalEndDate >= DateTime.UtcNow.Date); //all active rentals
            return rentals.ToList();
        }

        bool IRentalRepository.DoesUserHaveValidRentalForThisMovie(Guid movieId, Guid userId)
        {
            var hasRental = _context.Rentals.Any(r => r.UserId == userId &&
                                                      r.MovieId == movieId &&
                                                      r.RentalEndDate >= DateTime.UtcNow.Date);
            return hasRental;
        }
    }
}
