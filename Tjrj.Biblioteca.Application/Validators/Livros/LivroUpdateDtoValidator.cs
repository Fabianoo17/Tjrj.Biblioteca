using FluentValidation;
using Tjrj.Biblioteca.Application.Dtos.Livros;

namespace Tjrj.Biblioteca.Application.Validators.Livros
{
    public class LivroUpdateDtoValidator : AbstractValidator<LivroUpdateDto>
    {
        public LivroUpdateDtoValidator()
        {
            RuleFor(x => x.Codl)
                .GreaterThan(0).WithMessage("Código do livro inválido.");

            Include(new LivroCreateDtoValidator());
        }
    }
}
