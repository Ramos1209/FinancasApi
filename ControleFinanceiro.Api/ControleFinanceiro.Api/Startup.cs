using System.IO;
using ControleFinanceiro.Api.Extensions;
using ControleFinanceiro.Api.Validation;
using ControleFinanceiro.Api.ViewModels;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.DAL.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFinanceiro.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<FinanceiroContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Conection"));
            });

            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<FinanceiroContext>();

            services.ConfigurarSenhaUsuario();

            services.AddScoped<ICategoriaRpository, CategoriaRpository>();
            services.AddScoped<ITipoRepository, TipoRepository>();
            services.AddScoped<IFuncaoRepository, FuncaoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
            services.AddTransient<IValidator<FuncaoViewModel>, FuncaoViewModelValidator>();
            services.AddTransient<IValidator<RegistroViewModels>, RegistroViewModelValidator>();

            services.AddCors();
            services.AddSpaStaticFiles(diretorio =>
            {
                diretorio.RootPath = "ControleFinanceiro-ui";
            });
            

            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

        }
    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-ui");

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
                }
            });
        }
    }
}
