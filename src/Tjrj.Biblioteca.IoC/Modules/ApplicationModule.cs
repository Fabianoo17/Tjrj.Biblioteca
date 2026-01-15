using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // services.AddScoped<ILivroService, LivroService>();

            return services;
        }
    }
}
