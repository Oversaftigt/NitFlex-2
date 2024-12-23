﻿using Dapr.Client;
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

        [HttpPost("create")]
        public async Task<ActionResult> CreateRental([FromBody]CreateRentalItem createRentalItem)
        {
            try
            {
                //Check first if theres already a rental for this movie and this user
                RentalValidationRequest validationRequest = new() { MovieId = createRentalItem.MovieId, UserId = createRentalItem.UserId };
                bool alreadyRentedForUser = _rentalService.DoesUserHaveRentalForThisMovie(validationRequest);

                if (alreadyRentedForUser)
                {
                    return BadRequest("User already has an ongoing rent on this movie");
                }

                //otherwise create the rental
                _rentalService.CreateRental(createRentalItem);
                _logger.LogInformation("Creating Rental for UserID:" + createRentalItem.UserId + " with movie: " + createRentalItem.MovieId);
                return Ok($"{createRentalItem.UserId} is now renting {createRentalItem.MovieId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating rental", error = ex.Message });
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> CheckValidRental([FromBody] RentalValidationRequest request)
        {
            var isValidRental = _rentalService.DoesUserHaveRentalForThisMovie(request);
            return Ok(isValidRental);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> GetAllRentalsForUser(Guid userId)
        {
            var rentals = _rentalService.GetAllRentalByUser(userId);
            return Ok(rentals);
        }
    }
}
