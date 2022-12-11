using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Mappings
{
    /// <summary>
    /// Classe de mapeamento do AutoMapper
    /// </summary>
    public class CommandToEntityMap : Profile
    {
        public CommandToEntityMap()
        {
            CreateMap<CriarUsuarioCommand, Usuario>()
                .AfterMap((command, entity) => 
                {
                    entity.Id = Guid.NewGuid();
                    entity.DataHoraCriacao = DateTime.Now;
                });
        }
    }
}
