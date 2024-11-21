using MovieMicroservice.Models;

namespace MovieMicroservice.Services
{
    public interface IMovieService
    {
        void CreateMovie(CreateMovieItem createMovieItem);
        void EditMovie(EditMovieItem editMovieItem);
        void DeleteMovie(DeleteMovieItem deleteMovieItem);
        MovieItem GetMovieById(Guid id);
        List<MovieItem> GetAllMovies();
        string GetMovieLinkById(Guid movieId);
    }
}
