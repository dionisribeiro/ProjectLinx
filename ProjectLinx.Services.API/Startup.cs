using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Infra.Data.Context;
using ProjectLinx.Infra.Data.Repository;

namespace ProjectLinx.Services.API
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UsuarioDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ////Registrando serviço do Repository e criando uma instância do serviço a cada requisição
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            //services.AddAutoMapper();
            MapConfiguration.RegisterMapProfile();
            services.AddMvc();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}