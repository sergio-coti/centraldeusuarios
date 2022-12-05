using Bogus;
using Bogus.DataSets;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeUsuarios.UnitTests.Repositories
{
    /// <summary>
    /// Classe de teste para o repositório de usuários
    /// </summary>
    public class UsuarioRepositoryTest
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        public UsuarioRepositoryTest(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [Fact]
        public void TestCreate()
        {
            #region Realizando o cadastro de um usuário

            var usuario = CreateUsuario();

            #endregion

            #region Verificando se o usuário foi cadastrado

            var usuarioById = _usuarioRepository.GetById(usuario.Id);

            usuarioById.Should().NotBeNull();
            usuarioById.Nome.Should().Be(usuario.Nome);
            usuarioById.Email.Should().Be(usuario.Email);

            #endregion
        }

        [Fact]
        public void TestUpdate()
        {
            #region Realizando o cadastro de um usuário

            var usuario = CreateUsuario();

            #endregion

            #region Atualizando os dados do usuário

            var faker = new Faker("pt_BR");

            usuario.Nome = faker.Person.FullName;
            usuario.Email = faker.Internet.Email();

            _usuarioRepository.Update(usuario);

            #endregion

            #region Verificando se o usuário foi cadastrado

            var usuarioById = _usuarioRepository.GetById(usuario.Id);

            usuarioById.Should().NotBeNull();
            usuarioById.Nome.Should().Be(usuario.Nome);
            usuarioById.Email.Should().Be(usuario.Email);

            #endregion
        }

        [Fact]
        public void TestDelete()
        {
            #region Realizando o cadastro de um usuário

            var usuario = CreateUsuario();

            #endregion

            #region Excluindo o usuário

            _usuarioRepository.Delete(usuario);

            #endregion

            #region Verificando se o usuário não foi cadastrado

            var usuarioById = _usuarioRepository.GetById(usuario.Id);

            usuarioById.Should().BeNull();

            #endregion
        }

        [Fact]
        public void TestGetAll()
        {
            #region Realizando o cadastro de um usuário

            var usuario = CreateUsuario();

            #endregion

            #region Consultando todos os usuários do banco de dados

            var lista = _usuarioRepository.GetAll();

            #endregion

            #region Verificando se o usuário foi retornado na consulta

            lista.FirstOrDefault(u => u.Id.Equals(usuario.Id)).Should().NotBeNull();

            #endregion
        }

        [Fact]
        public void TestGetById()
        {
            #region Realizando o cadastro de um usuário

            var usuario = CreateUsuario();

            #endregion

            #region Consultando o usuário através do ID

            var usuarioById = _usuarioRepository.GetById(usuario.Id);

            #endregion

            #region Verificando se o usuário foi retornado na consulta

            usuarioById.Should().NotBeNull();
            usuarioById.Nome.Should().Be(usuario.Nome);
            usuarioById.Email.Should().Be(usuario.Email);

            #endregion
        }

        [Fact]
        public void TestGetByEmail()
        {
            #region Realizando o cadastro de um usuário

            var usuario = CreateUsuario();

            #endregion

            #region Consultando o usuário através do email

            var usuarioByEmail = _usuarioRepository.GetByEmail(usuario.Email);

            #endregion

            #region Verificando se o usuário foi retornado na consulta

            usuarioByEmail.Should().NotBeNull();
            usuarioByEmail.Nome.Should().Be(usuario.Nome);
            usuarioByEmail.Email.Should().Be(usuario.Email);

            #endregion
        }

        /// <summary>
        /// Método auxiliar para criar um usuário no reposítório
        /// </summary>
        private Usuario CreateUsuario()
        {
            var faker = new Faker("pt_BR");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = $"@{faker.Internet.Password(10)}",
                DataHoraCriacao = DateTime.Now
            };

            _usuarioRepository.Create(usuario);
            return usuario;
        }
    }
}
