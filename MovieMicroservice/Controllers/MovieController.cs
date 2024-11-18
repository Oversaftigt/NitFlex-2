using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using MovieMicroservice.Models;
using MovieMicroservice.Services;

namespace MovieMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;
        private readonly DaprClient _daprClient;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger, DaprClient daprClient)
        {
            _movieService = movieService;
            _logger = logger;
            _daprClient = daprClient;
        }


        [HttpPost]
        public async Task<ActionResult> CreateMovie(CreateMovieItem createMovieItem)
        {
            try
            {
                _movieService.CreateMovie(createMovieItem);
                _logger.LogInformation("Creating movie: " + createMovieItem.MovieName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "Error creating movie", error = ex.Message });
            }
        }
    }
}
