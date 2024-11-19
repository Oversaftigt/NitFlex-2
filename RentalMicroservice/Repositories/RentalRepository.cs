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
            throw new NotImplementedException();
        }

        bool IRentalRepository.DoesUserHaveRentalForThisMovie(Guid movieId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
