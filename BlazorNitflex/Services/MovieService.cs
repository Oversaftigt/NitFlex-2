using BlazorNitflex.Services.Interfaces;

namespace BlazorNitflex.Services
{
    public class MovieService : IMovieService
    {
        private IHttpClientFactory _httpClientFactory;

        public MovieService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



    }
}
