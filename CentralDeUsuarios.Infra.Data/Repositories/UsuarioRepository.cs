using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Repositories
{
    /// <summary>
    /// Classe para implementar o repositório de usuários
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario, Guid>, IUsuarioRepository
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        public UsuarioRepository(SqlServerContext sqlServerContext)
            : base(sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public override void Create(Usuario entity)
        {
            entity.Senha = MD5Helper.Encrypt(entity.Senha);
            base.Create(entity);
        }

        public override void Update(Usuario entity)
        {
            entity.Senha = MD5Helper.Encrypt(entity.Senha);
            base.Update(entity);
        }

        public Usuario GetByEmail(string email)
        {
            return _sqlServerContext.Usuario
                .FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
