using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalMicroservice.Models;
using RentalMicroservice.Services;

namespace RentalMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalController> _logger;
        private readonly DaprClient _daprClient;

        public RentalController(IRentalService rentalService, ILogger<RentalController> logger, DaprClient daprClient)
        {
            _rentalService = rentalService;
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental(CreateRentalItem createRentalItem)
        {
            try
            {
                _rentalService.CreateRental(createRentalItem);
                _logger.LogInformation("Creating Rental for UserID:" + createRentalItem.UserId + " with movie: " + createRentalItem.MovieId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating rental", error = ex.Message });
            }
        }
    }
}
