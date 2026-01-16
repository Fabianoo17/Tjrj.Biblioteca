using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Application.Services;

namespace Tjrj.Biblioteca.IoC.Modules
{
    internal static class ApplicationModule
    {
        internal static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            //services.AddFluentValidationAutoValidation();
            //services.AddValidatorsFromAssemblyContaining(typeof(LivroCreateDto));
            // Alternativa: typeof(AlgumValidator).Assembly

            // Quando criar services/usecases:
            
            
            services.AddScoped<ILivroService, LivroService>();
            services.AddValidatorsFromAssembly(typeof(Tjrj.Biblioteca.Application.Dtos.LivroCreateDto).Assembly);
            services.AddValidatorsFromAssembly(typeof(Tjrj.Biblioteca.Application.Dtos.LivroPrecoDto).Assembly);
            services.AddValidatorsFromAssembly(typeof(Tjrj.Biblioteca.Application.Dtos.LivroUpdateDto).Assembly);

            return services;
        }
    }
}
