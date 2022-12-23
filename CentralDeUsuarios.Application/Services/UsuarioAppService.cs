using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Models;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
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
        private readonly ILogUsuariosPersistence _logUsuariosPersistence;
        private readonly IMapper _mapper;

        public UsuarioAppService(IUsuarioDomainService usuarioDomainService, MessageQueueProducer messageQueueProducer, IMapper mapper, ILogUsuariosPersistence logUsuariosPersistence)
        {
            _usuarioDomainService = usuarioDomainService;
            _messageQueueProducer = messageQueueProducer;
            _mapper = mapper;
            _logUsuariosPersistence = logUsuariosPersistence;
        }

        public void CriarUsuario(CriarUsuarioCommand command)
        {
            #region Capturando e validando o usuário

            var usuario = _mapper.Map<Usuario>(command);

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
                    Email= usuario.Email,
                })
            };

            _messageQueueProducer.Create(_messageQueueModel);

            #endregion

            #region Gravando o log da operação

            var logUsuarioModel = new LogUsuarioModel
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id,
                DataHora = DateTime.Now,
                Operacao = "Criação de usuário",
                Detalhes = JsonConvert.SerializeObject(new { usuario.Nome, usuario.Email })
            };

            _logUsuariosPersistence.Create(logUsuarioModel);

            #endregion
        }

        public void Dispose()
        {
            _usuarioDomainService.Dispose();
        }
    }
}
