using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Notifications;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Infra.Logs.Models;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using FluentValidation;
using MediatR;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.RequestHandlers
{
    /// <summary>
    /// Classe para processar as requisições de usuário 
    /// da aplicação que serão orquestradas pelo CQRS (MediatR)
    /// </summary>
    public class UsuarioRequestHandler : 
        IRequestHandler<CriarUsuarioCommand>,
        IRequestHandler<AutenticarUsuarioCommand>,
        IDisposable
    {
        //atributos
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly MessageQueueProducer _messageQueueProducer;
        private readonly IMediator _mediatR;
        private readonly IMapper _mapper;

        //construtor para injeções de dependência
        public UsuarioRequestHandler(IUsuarioDomainService usuarioDomainService, MessageQueueProducer messageQueueProducer, IMediator mediatR, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _messageQueueProducer = messageQueueProducer;
            _mediatR = mediatR;
            _mapper = mapper;
        }

        /// <summary>
        /// Implementar o fluxo CQRS para criação de usuário
        /// </summary>
        public async Task<Unit> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            #region Capturando e validando o usuário

            var usuario = _mapper.Map<Usuario>(request);

            var validate = usuario.Validate;
            if (!validate.IsValid)
                throw new ValidationException(validate.Errors);

            #endregion

            #region Cadastrando o usuário

            _usuarioDomainService.CriarUsuario(usuario);

            #endregion

            #region Enviando uma mensagem para a fila

            var _messageQueueModel = new MessageQueueModel
            {
                Tipo = TipoMensagem.CONFIRMACAO_DE_CADASTRO,
                Conteudo = JsonConvert.SerializeObject(new UsuariosMessageVO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                })
            };

            _messageQueueProducer.Create(_messageQueueModel);

            #endregion

            #region Gravando o log da operação

            var logUsuariosNotification = new LogUsuariosNotification
            {
                LogUsuario = new LogUsuarioModel
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuario.Id,
                    DataHora = DateTime.Now,
                    Operacao = "Criação de usuário",
                    Detalhes = JsonConvert.SerializeObject(new { usuario.Nome, usuario.Email })
                }
            };

            await _mediatR.Publish(logUsuariosNotification);

            #endregion

            return Unit.Value;
        }

        /// <summary>
        /// Implementar o fluxo CQRS para autenticação de usuário
        /// </summary>
        public Task<Unit> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _usuarioDomainService.Dispose();
        }
    }
}
