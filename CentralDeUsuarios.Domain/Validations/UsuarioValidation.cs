using CentralDeUsuarios.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Validations
{
    /// <summary>
    /// Classe para validação da entidade Usuário
    /// </summary>
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(u => u.Id)
                .NotEmpty()
                .WithMessage("Id é obrigatório.");

            RuleFor(u => u.Nome)
                .NotEmpty()
                .Length(6, 150)
                .WithMessage("Nome de usuário inválido.");

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Endereço de email inválido.");

            RuleFor(u => u.Senha)
                .NotEmpty()
                .Length(8, 20).WithMessage("Senha deve ter de 8 a 20 caracteres.")
                .Matches(@"[A-Z]+").WithMessage("Senha deve ter pelo menos 1 letra maiúscula.")
                .Matches(@"[a-z]+").WithMessage("Senha deve ter pelo menos 1 letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("Senha deve ter pelo menos 1 número.")
                .Matches(@"[\!\?\*\.\@]+").WithMessage("Senha deve ter pelo menos 1 caractere especial (!?*.@).");
        }
    }
}
