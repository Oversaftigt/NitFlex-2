using MovieMicroservice.Models;
using MovieMicroservice.Repositories;
using MovieMicroservice.ThirdParty;

namespace MovieMicroservice.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IYoutubeService _youtubeService;

        public MovieService(IMovieRepository repository, IYoutubeService youtubeService)
        {
            this._movieRepository = repository;
            _youtubeService = youtubeService;
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

        async Task<string> IMovieService.GetMovieLinkById(Guid id)
        {
            var movie = _movieRepository.GetMovieById(id);
            var youtubeLink = await _youtubeService.GetFirstMovieLink(movie.MovieName);

            return  youtubeLink;
        }

        MovieItem IMovieService.GetMovieById(Guid id)
        {
            return _movieRepository.GetMovieById(id);
        }
    }
}
