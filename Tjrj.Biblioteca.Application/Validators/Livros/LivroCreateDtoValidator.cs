using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Application.Dtos.Livros;

namespace Tjrj.Biblioteca.Application.Validators.Livros
{
    public class LivroCreateDtoValidator : AbstractValidator<LivroCreateDto>
    {
        public LivroCreateDtoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório.")
                .MaximumLength(40).WithMessage("Título deve ter no máximo 40 caracteres.");

            RuleFor(x => x.Editora)
                .NotEmpty().WithMessage("Editora é obrigatória.")
                .MaximumLength(40).WithMessage("Editora deve ter no máximo 40 caracteres.");

            RuleFor(x => x.Edicao)
                .GreaterThan(0).WithMessage("Edição deve ser maior que zero.");

            RuleFor(x => x.AnoPublicacao)
                .NotEmpty().WithMessage("Ano de publicação é obrigatório.")
                .Length(4).WithMessage("Ano de publicação deve conter 4 dígitos.")
                .Matches("^[0-9]{4}$").WithMessage("Ano de publicação deve conter apenas números.");

            RuleFor(x => x.AutorIds)
                .NotEmpty().WithMessage("Informe ao menos um autor.");

            RuleForEach(x => x.Precos)
                .SetValidator(new LivroPrecoDtoValidator());

            RuleFor(x => x.Precos)
                .Must(p => p.Select(a => a.FormaCompraId).Distinct().Count() == p.Count)
                .WithMessage("Não repita forma de compra nos preços.");
        }
    }
}
