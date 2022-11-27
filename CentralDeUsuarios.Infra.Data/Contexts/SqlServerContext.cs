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
        /// Método para definir a connectionstring do projeto
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BD_CentralDeUsuarios;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
