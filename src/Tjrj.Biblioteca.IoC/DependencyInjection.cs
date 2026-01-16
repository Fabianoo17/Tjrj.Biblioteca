using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.IoC.Modules;

namespace Tjrj.Biblioteca.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCrossCuttingDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationDependencies();
            services.AddInfrastructureDependencies(configuration);
            return services;
        }
    }
}
