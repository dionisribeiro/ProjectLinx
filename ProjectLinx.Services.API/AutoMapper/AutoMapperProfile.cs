using AutoMapper;
using ProjectLinx.Domain;

namespace ProjectLinx.Services.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        }
    }
}