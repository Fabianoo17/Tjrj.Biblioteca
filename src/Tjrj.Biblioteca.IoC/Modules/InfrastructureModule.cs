using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Infra.Contexts;
using Tjrj.Biblioteca.Infra.Repositories;

namespace Tjrj.Biblioteca.IoC.Modules
{
    internal static class InfrastructureModule
    {
        internal static IServiceCollection AddInfrastructureDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaDbContext>(options =>
            {
                var cs = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(cs);
            });

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddScoped<IFormaCompraRepository, FormaCompraRepository>();

            return services;
        }
    }
}
