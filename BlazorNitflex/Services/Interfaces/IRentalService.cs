using BlazorNitflex.Models;

namespace BlazorNitflex.Services.Interfaces
{
    public interface IRentalService
    {
        Task<bool> CreateRental(CreateRentalItem createRentalItem);
        Task<List<RentalItem>> GetAllRentalByUser(Guid userId);
        Task<bool> DoesUserHaveRentalForThisMovie(RentalValidationRequest validationRequest);
    }
}
