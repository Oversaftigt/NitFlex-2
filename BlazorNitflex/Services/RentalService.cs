using BlazorNitflex.Models;
using BlazorNitflex.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorNitflex.Services
{
    public class RentalService : IRentalService
    {
        private IHttpClientFactory _httpClientFactory;

        public RentalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateRental(CreateRentalItem createRentalItem)
        {

            var httpclient = _httpClientFactory.CreateClient("rentalclient");

            var response = await httpclient.PostAsync("api/rental/create", JsonContent.Create(createRentalItem));

            if (response.IsSuccessStatusCode is true)
            {
                return true;
            }
            return false;

        }

        public Task<List<RentalItem>> GetAllRentalByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesUserHaveRentalForThisMovie(RentalValidationRequest rentalValidation)
        {
            var httpclient = _httpClientFactory.CreateClient("rentalclient");

            var response = await httpclient.PostAsync("api/rental/validate", JsonContent.Create(rentalValidation));

            if (response.IsSuccessStatusCode is true)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
