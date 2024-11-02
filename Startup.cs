using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hsf_Receitas.Services;
using Hsf_Receitas.Data;

namespace Hsf_Receitas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            // implementações para configuração do uso de sessão
            services.Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => false;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            services.AddSession();
            services.AddScoped<MedicacaoServices>();
            services.AddScoped<ReceituarioServices>();
            services.AddScoped<AtestadoMedicoServices>();
            services.AddScoped<AtestadoComparecimentoServices>();
            services.AddScoped<HSFContext>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logFac)
        {
            // Use este método para configurar as requisições HTTP do Pipeline.

            //Crie um objeto de IWbHostEnviroment para realizar requisições de arquivos e o adicione como paramêtro no metodo Configure(), crie também um ojeto de ILoggerFactory para utilizar a ferramenta de geração de logs e passe-o também como parâmetro ao metodo Configure();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //Https
                app.UseHsts();
            }

            //Https

            //Implementando recursos do uso de sessão ao Pipeline da aplicação
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseSession();

            //dotnet dev-certs https --trust, instala um certificado HTTPS para localhost

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Aplicando sistema de geração de Logs através da ferramenta Serilog.Extensions.Logging.File
            logFac.AddFile("Logs/log-{Date}.txt", LogLevel.Error);
        }
    }
}