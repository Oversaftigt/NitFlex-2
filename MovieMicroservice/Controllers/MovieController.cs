using Dapr.Client;
using Dapr.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieMicroservice.Models;
using MovieMicroservice.Models.DTOs;
using MovieMicroservice.Services;
using MovieMicroservice.Workflows;
using System.Security.Claims;

namespace MovieMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;
        private readonly DaprClient _daprClient;
        private readonly DaprWorkflowClient _daprWorkflowClient;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger, DaprClient daprClient, DaprWorkflowClient daprWorkflowClient)
        {
            _movieService = movieService;
            _logger = logger;
            _daprClient = daprClient;
            _daprWorkflowClient = daprWorkflowClient;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult> CreateMovie(CreateMovieItem createMovieItem)
        {
            try
            {
                _movieService.CreateMovie(createMovieItem);
                _logger.LogInformation("Creating movie: " + createMovieItem.MovieName);
                return Ok($"{createMovieItem.MovieName} has been added to NitFlex.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating movie", error = ex.Message });
            }
        }
        
        [HttpGet("movieId")]
        public async Task<IActionResult> GetMovieDetails(Guid movieId)
        {
            var movie = _movieService.GetMovieById(movieId);
            if (movie is not null) 
            { 
                return Ok(movie);
            }
            return BadRequest("Error getting movie details");
        }

        [HttpPost("fetch-movie")]
        public async Task<IActionResult> FetchMovieLink([FromBody] RentalValidationRequest request)
        {

            try
            {
                var workflowInstanceId = Guid.NewGuid().ToString();

                await _daprWorkflowClient.ScheduleNewWorkflowAsync(
                    "FetchMovieWorkflow",
                    workflowInstanceId,
                    request
                    );
                    

                WorkflowState state;

                do
                {
                    // Get the current state of the workflow
                    state = await _daprWorkflowClient.GetWorkflowStateAsync(workflowInstanceId);

                    if (state.RuntimeStatus == WorkflowRuntimeStatus.Completed)
                    {
                        // Deserialize the result to string!!!!!remember
                        var result = state.ReadOutputAs<string>();
                        return Ok(result);
                    }

                    if (state.RuntimeStatus == WorkflowRuntimeStatus.Failed)
                    {
                        return StatusCode(500, "Workflow failed: " + state.FailureDetails); //get exception error messages from activities
                    }

                    // Wait
                    await Task.Delay(1234);

                } while (state.RuntimeStatus == WorkflowRuntimeStatus.Running);

                return Accepted(new { Status = state.RuntimeStatus });
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occured getting the movie: {ex.Message}");
            }

        }

        //test
        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> test()
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var token = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

                // Create an HTTP client for the IdentityMicroservice
                var httpClient = DaprClient.CreateInvokeHttpClient("identitymicroservice");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                // Make the GET request
                var response = await httpClient.GetAsync("api/account/userId");

                //if (!response.IsSuccessStatusCode)
                //{
                //    return StatusCode((int)response.StatusCode, "Error calling IdentityMicroservice");
                //}

                // Read the response as a string
                var userId = await response.Content.ReadAsStringAsync();

                return Ok(userId);
            }
            catch (Exception ex)
            {

                throw new Exception("Error getting userId", ex);
            }
        }

        [HttpPost("test2")]
        public async Task<IActionResult> test2([FromBody] RentalValidationRequest input)
        {
            try
            {
                var isValidrental = await _daprClient.InvokeMethodAsync<RentalValidationRequest, bool>(
                    "rentalmicroservice",
                    "api/rental/validate",
                    input
                    );

                return Ok(isValidrental);
            }
            catch (Exception ex)
            {

                throw new Exception("Error validating rental", ex);
            }
        }
    }
}
