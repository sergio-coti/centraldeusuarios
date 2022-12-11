using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeUsuarios.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public IActionResult Post(CriarUsuarioCommand command)
        {
            _usuarioAppService.CriarUsuario(command);

            return StatusCode(201, new 
            { 
                message = "Usuário cadastrado com sucesso.",
                command
            });
        }
    }
}
