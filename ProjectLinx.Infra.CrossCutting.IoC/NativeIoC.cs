using Microsoft.Extensions.DependencyInjection;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Infra.Data.Repository;

namespace ProjectLinx.Infra.CrossCutting.IoC
{
    public class NativeIoC
    {
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        //    //Registrando serviço do Repository e criando uma instância do serviço a cada requisição
        //    services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        //}
    }
}
