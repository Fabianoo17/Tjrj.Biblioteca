using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Application.Dtos.Autores;

namespace Tjrj.Biblioteca.Application.Validators.Autores
{
    public class AutorCreateDtoValidator : AbstractValidator<AutorCreateDto>
    {
        public AutorCreateDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome do autor é obrigatório.")
                .MaximumLength(40).WithMessage("Nome do autor deve ter no máximo 40 caracteres.");
        }
    }

    public class AutorUpdateDtoValidator : AbstractValidator<AutorUpdateDto>
    {
        public AutorUpdateDtoValidator()
        {
            RuleFor(x => x.CodAu)
                .GreaterThan(0).WithMessage("Código do autor inválido.");

            Include(new AutorCreateDtoValidator());
        }
    }
}
