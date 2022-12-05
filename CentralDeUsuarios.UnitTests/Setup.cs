using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Services;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.UnitTests
{
    /// <summary>
    /// Classe para configuração da Injeção de dependência no projeto XUnit
    /// </summary>
    public class Setup : Xunit.Di.Setup
    {
        //método para configurar a injeção de dependência
        protected override void Configure()
        {
            ConfigureAppConfiguration((hostingContext, config) => 
            {
                #region Ativar a Injeção de dependência no XUnit

                bool reloadOnChange = hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", true);
                if (hostingContext.HostingEnvironment.IsDevelopment())
                    config.AddUserSecrets<Setup>(true, reloadOnChange);

                #endregion
            });           

            ConfigureServices((context, services) =>
            {
                #region Localizar o arquivo appsettings.json

                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);

                #endregion

                #region Capturar a connectionstring do arquivo appsettings.json

                var root = configurationBuilder.Build();
                var connectionString = root.GetSection("ConnectionStrings").GetSection("CentralDeUsuarios").Value;

                #endregion

                #region Fazendo as injeção de dependência do projeto de teste

                //Injetando a connection string na classe SqlServerContext
                services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));

                //Injetando a classe UsuarioRepository na interface IUsuarioRepository
                services.AddTransient<IUsuarioRepository, UsuarioRepository>();

                //Injetando a classe UsuarioDomainService na interface IUsuarioDomainService
                services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();

                #endregion
            });
        }
    }
}
