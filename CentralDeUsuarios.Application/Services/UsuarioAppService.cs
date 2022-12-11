using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Services
{
    /// <summary>
    /// Implementação dos serviços de aplicação
    /// </summary>
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly MessageQueueProducer _messageQueueProducer;
        private readonly IMapper _mapper;

        public UsuarioAppService(IUsuarioDomainService usuarioDomainService, MessageQueueProducer messageQueueProducer, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _messageQueueProducer = messageQueueProducer;
            _mapper = mapper;
        }

        public void CriarUsuario(CriarUsuarioCommand command)
        {
            //gerando um usuário a partir do command (automapper)
            var usuario = _mapper.Map<Usuario>(command);

            //executar a validação do usuário
            var validate = usuario.Validate;

            if (!validate.IsValid)
                throw new ValidationException(validate.Errors);

            //criando o usuário
            _usuarioDomainService.CriarUsuario(usuario);

            //Criando o conteudo da mensagem que será enviada para a fila
            var _messageQueueModel = new MessageQueueModel
            {
                Conteudo = JsonConvert.SerializeObject(usuario)
            };

            //enviando mensagem para a fila
            _messageQueueProducer.Create(_messageQueueModel);
        }

        public void Dispose()
        {
            _usuarioDomainService.Dispose();
        }
    }
}
