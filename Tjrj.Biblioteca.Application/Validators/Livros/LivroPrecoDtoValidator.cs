using FluentValidation;
using Tjrj.Biblioteca.Application.Dtos;

namespace Tjrj.Biblioteca.Application.Validators.Livros
{
    public class LivroPrecoDtoValidator : AbstractValidator<LivroPrecoDto>
    {
        public LivroPrecoDtoValidator()
        {
            RuleFor(x => x.FormaCompraId)
                .GreaterThan(0).WithMessage("Forma de compra inválida.");

            RuleFor(x => x.Valor)
                .GreaterThanOrEqualTo(0).WithMessage("Valor não pode ser negativo.");
        }
    }
}
