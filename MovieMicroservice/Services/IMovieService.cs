using MovieMicroservice.Models;

namespace MovieMicroservice.Services
{
    public interface IMovieService
    {
        void CreateMovie(CreateMovieItem createMovieItem);
        void EditMovie(EditMovieItem editMovieItem);
        void DeleteMovie(DeleteMovieItem deleteMovieItem);
        List<MovieItem> GetAllMovies();
    }
}
