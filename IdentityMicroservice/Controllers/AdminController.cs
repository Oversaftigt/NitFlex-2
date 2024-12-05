using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMicroservice.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //for testing
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You are now in the Admin controller");
        }
    }
}
