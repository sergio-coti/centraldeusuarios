using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Producers
{
    /// <summary>
    /// Classe para escrita de mensagens na fila do RabbitMQ
    /// </summary>
    public class MessageQueueProducer
    {
        private readonly MessageSettings? _messageSettings;
        private readonly ConnectionFactory? _connectionFactory;

        public MessageQueueProducer(IOptions<MessageSettings> messageSettings)
        {
            _messageSettings = messageSettings.Value;

            //definindo a conexão com o servidor de mensageria (broker)
            _connectionFactory = new ConnectionFactory
            {
                HostName = _messageSettings.Host,
                UserName = _messageSettings.Username,
                Password = _messageSettings.Password,
            };
        }

        /// <summary>
        /// Método para escrever uma mensagem na fila
        /// </summary>
        public void Create(MessageQueueModel model)
        {
            //abrir conexão com o servidor de mensageria
            using (var connection = _connectionFactory.CreateConnection())
            {
                //criando um objeto na fila de mensagens
                using (var channel = connection.CreateModel())
                {
                    //parametros para conexão da fila
                    channel.QueueDeclare(
                        queue: _messageSettings.Queue, //Nome da fila
                        durable: true, //não apagar a fila quando o RabbitMQ for desligado
                        exclusive: false, //só permite uma conexão por vez na fila
                        autoDelete: false, //não excluir dados automaticamente, só o consumer pode excluir
                        arguments: null //sem argumentos
                        );

                    //escrevendo o conteudo da fila
                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _messageSettings.Queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))
                        );
                }
            }
        }
    }
}
