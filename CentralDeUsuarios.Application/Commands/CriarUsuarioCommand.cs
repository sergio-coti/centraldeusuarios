using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Commands
{
    /// <summary>
    /// Modelo de dados para a requisição de cadastro de usuário
    /// </summary>
    public class CriarUsuarioCommand
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
