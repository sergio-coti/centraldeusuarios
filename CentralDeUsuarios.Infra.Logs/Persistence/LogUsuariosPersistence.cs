using CentralDeUsuarios.Infra.Logs.Contexts;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Persistence
{
    /// <summary>
    /// Implementação da persistencia de dados para log de usuários
    /// </summary>
    public class LogUsuariosPersistence : ILogUsuariosPersistence
    {
        private readonly MongoDBContext _mongoDBContext;

        public LogUsuariosPersistence(MongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public void Create(LogUsuarioModel model)
        {
            _mongoDBContext.LogUsuarios.InsertOne(model);
        }

        public void Update(LogUsuarioModel model)
        {
            var filter = Builders<LogUsuarioModel>.Filter
                .Eq(log => log.Id, model.Id);

            _mongoDBContext.LogUsuarios.ReplaceOne(filter, model); //UpdateOne
        }

        public void Delete(LogUsuarioModel model)
        {
            var filter = Builders<LogUsuarioModel>.Filter
                .Eq(log => log.Id, model.Id);

            _mongoDBContext.LogUsuarios.DeleteOne(filter);
        }

        public List<LogUsuarioModel> GetAll(DateTime dataMin, DateTime dataMax)
        {
            var filter = Builders<LogUsuarioModel>.Filter
                .Where(log => log.DataHora >= dataMin && log.DataHora <= dataMax);

            return _mongoDBContext.LogUsuarios
                .Find(filter)
                .SortByDescending(log => log.DataHora)
                .ToList();
        }

        public List<LogUsuarioModel> GetAll(Guid usuarioId)
        {
            var filter = Builders<LogUsuarioModel>.Filter
                .Eq(log => log.UsuarioId, usuarioId);

            return _mongoDBContext.LogUsuarios
                .Find(filter)
                .SortByDescending(log => log.DataHora)
                .ToList();
        }
    }
}
