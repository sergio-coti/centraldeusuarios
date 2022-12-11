using CentralDeUsuarios.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Interfaces
{
    /// <summary>
    /// Interface para abstração dos métodos da camada de aplicação para usuário
    /// </summary>
    public interface IUsuarioAppService : IDisposable
    {
        /// <summary>
        /// Método para criar um usuário na aplicação
        /// </summary>
        /// <param name="command">Dados para criação do usuário</param>
        void CriarUsuario(CriarUsuarioCommand command);
    }
}
