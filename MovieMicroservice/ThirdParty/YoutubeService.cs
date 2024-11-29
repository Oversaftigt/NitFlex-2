using MovieMicroservice.Models;

namespace MovieMicroservice.ThirdParty
{
    public class YoutubeService : IYoutubeService
    {
        private readonly HttpClient _httpClient;

        private const string YoutubeApiKey = "AIzaSyA3tu8TiQRdLECYThZzZBg0oeawyQUt1EU";

        public YoutubeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFirstMovieLink(string searchString)
        {
            try
            {

                var youtubeEndpoint = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q={searchString}&type=video&key={YoutubeApiKey}";

                var response = await _httpClient.GetAsync(youtubeEndpoint);

                response.EnsureSuccessStatusCode();

                var searchResultList = await response.Content.ReadFromJsonAsync<YoutubeSearchResponse>();

                var videoId = searchResultList?.Items?.FirstOrDefault()?.Id?.VideoId;

                if (videoId != null)
                {
                    return $"https://www.youtube.com/embed/{videoId}";
                }
                //return rickroll if error happens.
                return $"https://www.youtube.com/embed/dQw4w9WgXcQ";
            }
            catch (Exception)
            {
                return $"https://www.youtube.com/embed/dQw4w9WgXcQ";
            }
        }
    }
}
