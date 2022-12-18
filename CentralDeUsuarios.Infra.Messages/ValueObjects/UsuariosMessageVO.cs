using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.ValueObjects
{
    /// <summary>
    /// Objeto de valor para gravar dados de usuário na mensagem da fila
    /// </summary>
    public class UsuariosMessageVO
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
