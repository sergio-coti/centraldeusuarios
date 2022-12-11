using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Models
{
    /// <summary>
    /// Classe para modelar as mensagens que serão escritas na fila
    /// </summary>
    public class MessageQueueModel
    {
        /// <summary>
        /// Identificador da mensagem na fila
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Conteudo da mensagem na fila
        /// </summary>
        public string? Conteudo { get; set; }

        /// <summary>
        /// Data e hora de escrita da mensagem na fila
        /// </summary>
        public DateTime? DataHoraCriacao { get; set; } = DateTime.Now;
    }
}
