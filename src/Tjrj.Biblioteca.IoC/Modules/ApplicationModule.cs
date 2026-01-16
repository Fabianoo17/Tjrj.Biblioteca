using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;
using Tjrj.Biblioteca.Application.Dtos.Autores;
using Tjrj.Biblioteca.Application.Dtos.Livros;
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
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IAssuntoService, AssuntoService>();
            services.AddScoped<IFormaCompraService, FormaCompraService>();
            services.AddScoped<IRelatorioService, RelatorioService>();

            //Livros
            services.AddValidatorsFromAssembly(typeof(LivroCreateDto).Assembly);
            services.AddValidatorsFromAssembly(typeof(LivroPrecoDto).Assembly);
            services.AddValidatorsFromAssembly(typeof(LivroUpdateDto).Assembly);

            //Autores
            services.AddValidatorsFromAssembly(typeof(AutorCreateDto).Assembly);
            services.AddValidatorsFromAssembly(typeof(AutorUpdateDto).Assembly);

            //Assuntos
            services.AddValidatorsFromAssembly(typeof(AssuntoCreateDto).Assembly);
            services.AddValidatorsFromAssembly(typeof(AssuntoUpdateDto).Assembly);



            return services;
        }
    }
}
