using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Domain;

namespace ProjectLinx.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public ActionResult<List<UsuarioViewModel>> Get()
        {
            var usuarioViewModel = Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(_usuarioRepository.GetAll().ToList());
            return usuarioViewModel;
        }
    }
}