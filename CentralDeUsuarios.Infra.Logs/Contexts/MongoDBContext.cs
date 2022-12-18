using CentralDeUsuarios.Infra.Logs.Models;
using CentralDeUsuarios.Infra.Logs.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Contexts
{
    /// <summary>
    /// Classe para conexão com o MongoDB
    /// </summary>
    public class MongoDBContext
    {
        private readonly MongoDBSettings? _mongoDBSettings;
        private IMongoDatabase _mongoDatabase;

        public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings.Value;

            #region Conectando no banco de dados

            var client = MongoClientSettings.FromUrl(new MongoUrl(_mongoDBSettings.Host));

            if (_mongoDBSettings.IsSSL)
                client.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                };

            _mongoDatabase = new MongoClient(client).GetDatabase(_mongoDBSettings.Name);

            #endregion
        }

        /// <summary>
        /// Propriedade para mepear a coleção do MongoDB
        /// </summary>
        public IMongoCollection<LogUsuarioModel> LogUsuarios
            => _mongoDatabase.GetCollection<LogUsuarioModel>("LogUsuarios");
    }
}
