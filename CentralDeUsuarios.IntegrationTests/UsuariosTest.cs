using Bogus;
using CentralDeUsuarios.Application.Commands;
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
        [Fact]
        public async Task Test_Post_Usuarios_Returns_Created()
        {
            var faker = new Faker("pt_BR");

            var request = new CriarUsuarioCommand
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = $"@1{faker.Internet.Password(8)}"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), 
                Encoding.UTF8, "application/json");

            var result = await new WebApplicationFactory<Program>()
                .CreateClient().PostAsync("/api/usuarios", content);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.Created);
        }
    }
}
