using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Services
{
    /// <summary>
    /// Implementação dos serviços de domínio de usuários
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Método para criar um usuário na aplicação
        /// </summary>
        /// <param name="usuario">Entidade de domínio</param>
        public void CriarUsuario(Usuario usuario)
        {
            //Não é permitido cadastrar usuários com o mesmo email

            DomainException.When(
                    _usuarioRepository.GetByEmail(usuario.Email) != null,
                    $"O email {usuario.Email} já está cadastrado, tente outro."
                );

            _usuarioRepository.Create(usuario);
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }
    }
}
