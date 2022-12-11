using CentralDeUsuarios.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeUsuarios.UnitTests.Entities
{
    /// <summary>
    /// Classe de teste para a entidade Usuário
    /// </summary>
    public class UsuarioTest
    {
        [Fact]
        public void ValidarIdTest()
        {
            var usuario = new Usuario
            {
                Id = Guid.Empty
            };

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Id é obrigatório"))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ValidarNomeTest()
        {
            var usuario = new Usuario
            {
                Nome = string.Empty
            };

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Nome de usuário inválido"))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ValidarEmailTest()
        {
            var usuario = new Usuario
            {
                Email = string.Empty
            };

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Endereço de email inválido"))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ValidarSenhaTest()
        {
            var usuario = new Usuario();

            usuario.Senha = string.Empty;

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter de 8 a 20 caracteres"))
                .Should()
                .NotBeNull();

            usuario.Senha = "adminadmin";

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter pelo menos 1 letra maiúscula"))
                .Should()
                .NotBeNull();

            usuario.Senha = "ADMINADMIN";

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter pelo menos 1 letra minúscula"))
                .Should()
                .NotBeNull();

            usuario.Senha = "adminADMIN";

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter pelo menos 1 número"))
                .Should()
                .NotBeNull();

            usuario.Senha = "Admin1234";

            usuario.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter pelo menos 1 caractere especial"))
                .Should()
                .NotBeNull();
        }
    }
}
