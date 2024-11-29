using BlazorNitflex.Models;
using System.ComponentModel;

namespace BlazorNitflex.Services.Interfaces
{
    public interface IMovieService
    {

        Task<List<MovieItem>> GetAllMovies();
        Task<bool> CreateMovie(CreateMovieItem createMovieItem);
    }
}
