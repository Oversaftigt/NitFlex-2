using MovieMicroservice.Models;
using MovieMicroservice.Repositories;

namespace MovieMicroservice.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository repository)
        {
            this._movieRepository = repository;
        }

        void IMovieService.CreateMovie(CreateMovieItem createMovieItem)
        {
            _movieRepository.CreateMovie(createMovieItem);
        }

        void IMovieService.EditMovie(EditMovieItem editMovieItem)
        {
            _movieRepository.EditMovie(editMovieItem);
        }

        void IMovieService.DeleteMovie(DeleteMovieItem deleteMovieItem)
        {
            _movieRepository.DeleteMovie(deleteMovieItem);
        }

        List<MovieItem> IMovieService.GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }
    }
}
