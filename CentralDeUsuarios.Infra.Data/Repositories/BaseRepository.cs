using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Repositories
{
    /// <summary>
    /// Classe genérica para repositório
    /// </summary>
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        protected BaseRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public virtual void Create(TEntity entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Added;
            _sqlServerContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Deleted;
            _sqlServerContext.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            return _sqlServerContext.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(TKey id)
        {
            return _sqlServerContext.Set<TEntity>().Find(id);
        }

        public void Dispose()
        {
            _sqlServerContext.Dispose();
        }
    }
}
