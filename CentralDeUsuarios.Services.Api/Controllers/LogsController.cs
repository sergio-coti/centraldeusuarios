using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeUsuarios.Services.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //TODO
            return StatusCode(200);
        }
    }
}
