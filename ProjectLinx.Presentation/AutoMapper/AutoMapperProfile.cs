using AutoMapper;
using ProjectLinx.Domain;
using ProjectLinx.Presentation.ViewModels;

namespace ProjectLinx.Presentation.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        }
    }
}
