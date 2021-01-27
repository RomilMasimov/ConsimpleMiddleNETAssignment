using Microsoft.AspNetCore.Mvc;

namespace ConsimpleMiddleNetAssignment.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class ShopController : ControllerBase
    {
        // GET
        [HttpGet]
        public IActionResult Ping()
        {
            return new JsonResult(new
            {
                Test = "Ok"
            });
        }
    }
}