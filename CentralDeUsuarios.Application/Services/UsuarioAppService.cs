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
using MediatR;
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
        //atributos
        private readonly IMediator _mediatR;

        //construtor para injeção de dependência
        public UsuarioAppService(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public async Task CriarUsuario(CriarUsuarioCommand command)
        {
            await _mediatR.Send(command);
        }

        public async Task AutenticarUsuario(AutenticarUsuarioCommand command)
        {
            await _mediatR.Send(command);
        }
    }
}
