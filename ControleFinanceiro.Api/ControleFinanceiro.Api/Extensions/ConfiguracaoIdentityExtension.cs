using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFinanceiro.Api.Extensions
{
    public static class ConfiguracaoIdentityExtension
    {
        public static void ConfigurarSenhaUsuario(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(op =>
            {
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequiredLength = 6;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
