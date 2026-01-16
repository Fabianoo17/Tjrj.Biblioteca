using FluentValidation;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;

namespace Tjrj.Biblioteca.Application.Validators.Assuntos
{
    public class AssuntoCreateDtoValidator : AbstractValidator<AssuntoCreateDto>
    {
        public AssuntoCreateDtoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição do assunto é obrigatória.")
                .MaximumLength(20).WithMessage("Descrição do assunto deve ter no máximo 20 caracteres.");
        }
    }

    public class AssuntoUpdateDtoValidator : AbstractValidator<AssuntoUpdateDto>
    {
        public AssuntoUpdateDtoValidator()
        {
            RuleFor(x => x.CodAs)
                .GreaterThan(0).WithMessage("Código do assunto inválido.");

            Include(new AssuntoCreateDtoValidator());
        }
    }
}
