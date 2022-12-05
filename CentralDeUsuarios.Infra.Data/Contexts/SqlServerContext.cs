using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Contexts
{
    /// <summary>
    /// Classe de contexto do banco de dados
    /// </summary>
    public class SqlServerContext : DbContext
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="dbContextOptions">Classe do EF para opções de configuração do DbContext</param>
        public SqlServerContext(DbContextOptions<SqlServerContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        /// <summary>
        /// Método para adicionar cada classe de mapeamento
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        /// <summary>
        /// Propriedade para fornecer os métodos que serão
        /// utilizados no repositório de usuários
        /// </summary>
        public DbSet<Usuario> Usuario { get; set; }
    }
}
