using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Settings
{
    /// <summary>
    /// Configurações para conexão no servidor de mensageria
    /// </summary>
    public class MessageSettings
    {
        public string? Host { get; set; }
        public string? Queue { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
