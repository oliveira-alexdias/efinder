using EFinder.API.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFinder.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [Route("find")]
        [HttpGet]
        public IActionResult Get([FromQuery] EmailFindRequest request)
        {
            return Ok(45);
        }
    }
}
