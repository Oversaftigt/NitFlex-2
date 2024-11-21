using MovieMicroservice.Models;

namespace MovieMicroservice.Repositories
{
    public interface IMovieRepository
    {
        void CreateMovie(CreateMovieItem createMovieItem);
        void EditMovie(EditMovieItem editMovieItem);
        void DeleteMovie(DeleteMovieItem deleteMovieItem);
        MovieItem GetMovieById(Guid id);
        List<MovieItem> GetAllMovies();

    }
}
