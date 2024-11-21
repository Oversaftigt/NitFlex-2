using MovieMicroservice.Data;
using MovieMicroservice.Models;

namespace MovieMicroservice.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext context)
        {
            _context = context;
        }

        void IMovieRepository.CreateMovie(CreateMovieItem createMovieItem)
        {
            var movie = new MovieItem
            {
                MovieDescription = createMovieItem.MovieDescription,
                MovieDurationInMinutes = createMovieItem.MovieDurationInMinutes,
                MovieName = createMovieItem.MovieName,
                MovieGenre = createMovieItem.MovieGenre,
                MovieRentalPrice = createMovieItem.MovieRentalPrice
            };
            _context.Add(movie);
            _context.SaveChanges();
        }

        void IMovieRepository.EditMovie(EditMovieItem editMovieItem)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == editMovieItem.Id);
            if (movie is not null)
            {
                movie.MovieName = editMovieItem.MovieName;
                movie.MovieGenre = editMovieItem.MovieGenre;
                movie.MovieRentalPrice = editMovieItem.MovieRentalPrice;
                movie.MovieDurationInMinutes = editMovieItem.MovieDurationInMinutes;
                movie.MovieDescription = editMovieItem.MovieDescription;

                _context.SaveChanges();
            }

        }

        void IMovieRepository.DeleteMovie(DeleteMovieItem deleteMovieItem)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == deleteMovieItem.Id);
            if (movie is not null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }

        List<MovieItem> IMovieRepository.GetAllMovies()
        {
            var allMovies = _context.Movies.ToList();
            return allMovies;
        }

        MovieItem IMovieRepository.GetMovieById(Guid id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            return movie;
            
        }
    }
}
