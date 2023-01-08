using Bogus;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeUsuarios.IntegrationTests
{
    public class UsuariosTest
    {
        private readonly TestHelper _testHelper;

        public UsuariosTest()
        {
            _testHelper = new TestHelper();
        }

        [Fact]
        public async Task Test_Post_Usuarios_Returns_Created()
        {
            var faker = new Faker("pt_BR");

            var command = new CriarUsuarioCommand
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = $"@1{faker.Internet.Password(8)}"
            };

            var content = _testHelper.CreateContent(command);
            var result = await _testHelper.CreateClient().PostAsync("/api/usuarios", content);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.Created);
        }
    }
}
