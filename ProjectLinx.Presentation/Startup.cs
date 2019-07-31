using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Infra.Data.Context;
using ProjectLinx.Infra.Data.Repository;
using ProjectLinx.Presentation.AutoMapper;

namespace ProjectLinx.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Classe que inicia o Automapper
        public class MapConfiguration
        {
            public static void RegisterMapProfile()
            {
                Mapper.Initialize(c =>
                {
                    c.AddProfile<AutoMapperProfile>();
                });
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<UsuarioDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ////Registrando serviço do Repository e criando uma instância do serviço a cada requisição
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            MapConfiguration.RegisterMapProfile();

            //Adicionando serviço de Cookies a aplicação
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                            options.LoginPath = "/Login/Index"
                );

            services.AddMvc();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Usuario}/{action=Index}/{id?}");
            });
        }
    }
}