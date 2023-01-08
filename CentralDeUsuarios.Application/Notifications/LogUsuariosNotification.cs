using CentralDeUsuarios.Infra.Logs.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Notifications
{
    /// <summary>
    /// Define o que será escrito/gravado na base de cache/consulta
    /// INotification : Interface do MediatR que qualifica a classe como uma
    /// classe que contem o modelo dos dados que serão gravados em cache
    /// </summary>
    public class LogUsuariosNotification : INotification
    {
        /// <summary>
        /// Modelo de dados que será gravado na base de cache/consulta
        /// </summary>
        public LogUsuarioModel? LogUsuario { get; set; }
    }
}
