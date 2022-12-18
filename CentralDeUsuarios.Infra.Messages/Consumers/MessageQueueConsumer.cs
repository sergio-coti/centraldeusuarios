using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Settings;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Consumers
{
    /// <summary>
    /// Classe para consumir e processar as mensagens da fila
    /// </summary>
    public class MessageQueueConsumer : BackgroundService
    {
        private readonly MessageSettings? _messageSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly MailHelper _mailHelper;
        private readonly IConnection? _connection;
        private readonly IModel? _model;

        public MessageQueueConsumer(IOptions<MessageSettings> messageSettings, IServiceProvider serviceProvider, MailHelper mailHelper)
        {
            _messageSettings = messageSettings.Value;
            _serviceProvider = serviceProvider;
            _mailHelper = mailHelper;

            #region Conectando no servidor de mensageria

            var connectionFactory = new ConnectionFactory
            {
                HostName = _messageSettings.Host,
                UserName = _messageSettings.Username,
                Password = _messageSettings.Password,
            };

            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(
                queue: _messageSettings.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            #endregion
        }

        /// <summary>
        /// Método para ler a fila do RabbitMQ
        /// </summary>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //componente para fazer a leitura da fila
            var consumer = new EventingBasicConsumer(_model);

            //fazendo a leitura
            consumer.Received += (sender, args) =>
            {
                //ler o conteudo da mensagem gravada na fila
                var contentArray = args.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                //deserializar a mensagem
                var messageQueueModel = JsonConvert.DeserializeObject<MessageQueueModel>(contentString);

                //verificar o tipo da mensagem
                switch(messageQueueModel.Tipo)
                {
                    case TipoMensagem.CONFIRMACAO_DE_CADASTRO:

                        //processando a mensagem
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            //capturando os dados do usuario contido na mensagem
                            var usuariosMessageVO = JsonConvert.DeserializeObject<UsuariosMessageVO>(messageQueueModel.Conteudo);

                            //enviando o email
                            EnviarMensagemDeConfirmacaoDeCadastro(usuariosMessageVO);

                            //comunicando ao rabbit que a mensagem foi processada!
                            //dessa forma, a mensagem sairá da fila
                            _model.BasicAck(args.DeliveryTag, false);
                        }

                        break;

                    case TipoMensagem.RECUPERACAO_DE_SENHA:
                        //TODO
                        break;
                }
            };

            //executando o consumidor
            _model.BasicConsume(_messageSettings.Queue, false, consumer);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Método para escrever e enviar o email de confirmação de cadastro de conta de usuário
        /// </summary>
        private void EnviarMensagemDeConfirmacaoDeCadastro(UsuariosMessageVO usuariosMessageVO)
        {
            var mailTo = usuariosMessageVO.Email;
            var subject = $"Confirmação de cadastro de usuário. ID: {usuariosMessageVO.Id}";
            var body = $@"
                Olá {usuariosMessageVO.Nome},
                <br/>
                <br/>
                <strong>Parabéns, sua conta de usuário foi criada com sucesso!</strong>
                <br/>
                <br/>
                ID: <strong>{usuariosMessageVO.Id}</strong> <br/>
                Nome: <strong>{usuariosMessageVO.Nome}</strong> <br/>
                <br/>
                Att, <br/>
                Equipe COTI Informática.
            ";

            _mailHelper.Send(mailTo, subject, body);
        }
    }
}
