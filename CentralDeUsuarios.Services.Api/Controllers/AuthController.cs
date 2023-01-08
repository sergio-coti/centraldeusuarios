using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeUsuarios.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public AuthController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AutenticarUsuarioCommand command)
        {
            await _usuarioAppService.AutenticarUsuario(command);

            return StatusCode(200, new { /* TODO */ });
        }
    }
}
