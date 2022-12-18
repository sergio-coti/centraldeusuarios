using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Application.Services;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Services;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Repositories;
using CentralDeUsuarios.Infra.Logs.Contexts;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Persistence;
using CentralDeUsuarios.Infra.Logs.Settings;
using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.EntityFrameworkCore;

namespace CentralDeUsuarios.Services.Api
{
    /// <summary>
    /// Classe para configuração o contexto de injeção de dependência do AspNet
    /// </summary>
    public static class Setup
    {
        public static void AddRegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();
            builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        public static void AddEntityFrameworkServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("CentralDeUsuarios");
            builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddMessageServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MessageSettings>(builder.Configuration.GetSection("MessageSettings"));
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddTransient<MessageQueueProducer>();
            builder.Services.AddTransient<MailHelper>();
        }

        public static void AddAutoMapperServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddMongoDBServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddSingleton<MongoDBContext>();
            builder.Services.AddTransient<ILogUsuariosPersistence, LogUsuariosPersistence>();
        }
    }
}
