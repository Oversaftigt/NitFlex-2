
namespace MovieMicroservice.ThirdParty
{
    public interface IYoutubeService
    {
        Task<string> GetFirstMovieLink(string searchstring);
    }
}
