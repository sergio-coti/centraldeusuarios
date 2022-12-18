using CentralDeUsuarios.Infra.Logs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Interfaces
{
    /// <summary>
    /// Interface para operações no MongoDB para collection LogUsuarios
    /// </summary>
    public interface ILogUsuariosPersistence
    {
        void Create(LogUsuarioModel model);
        void Update(LogUsuarioModel model); 
        void Delete(LogUsuarioModel model);

        List<LogUsuarioModel> GetAll(DateTime dataMin, DateTime dataMax);
        List<LogUsuarioModel> GetAll(Guid usuarioId);
    }
}
