using BlazorNitflex.Models;
using BlazorNitflex.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorNitflex.Services
{
    public class MovieService : IMovieService
    {
        private IHttpClientFactory _httpClientFactory;

        public MovieService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<MovieItem>> GetAllMovies()
        {
            try
            {
                var httpclient = _httpClientFactory.CreateClient("movieclient");

                var movies = await httpclient.GetFromJsonAsync<List<MovieItem>>("api/movie/allmovies");

                return movies;
            }
            catch (Exception ex)
            {

                return new List<MovieItem>();
            }

        }

        public async Task<bool> CreateMovie(CreateMovieItem createMovieItem)
        {
            try
            {
                var httpclient = _httpClientFactory.CreateClient("movieclient");

                var response = await httpclient.PostAsync("api/movie/create", JsonContent.Create(createMovieItem));

                if (response.IsSuccessStatusCode is true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> GetMovieLink(RentalValidationRequest request)
        {
            var httpclient = _httpClientFactory.CreateClient("movieclient");

            var response = await httpclient.PostAsync("api/movie/fetch-movie", JsonContent.Create(request));

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
