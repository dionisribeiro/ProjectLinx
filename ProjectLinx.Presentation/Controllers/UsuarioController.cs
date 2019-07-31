using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Domain;
using ProjectLinx.Presentation.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ProjectLinx.Presentation.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Pesquisar(string busca)
        {
            if (string.IsNullOrEmpty(busca) || busca.Contains("%%"))
            {
                var usuarioViewModel = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioRepository.GetAll());
                return View("Index", usuarioViewModel);
            }
            var pesquisarUsuario = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioRepository.GetUsuarios(busca));

            return View("Index", pesquisarUsuario);
        }
                
        public IActionResult Index()
        {
            var nomeLogado = _usuarioRepository.GetByNameUser(HttpContext.User.Identity.Name);
            ViewBag.nomeLogado = nomeLogado;

            //Configurações da API
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44336/api/usuario/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            //Consulta a API e obtem o retorno
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //Leitura do JSON que é retornado do resutado da consulta da API
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                //recebendo o resultado do JSON
                var result = streamReader.ReadToEnd();

                //Convertendo o resultado JSON para IEnumerable de UsuarioViewModel
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(result);
                return View(usuarios);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var nomeLogado = _usuarioRepository.GetByNameUser(HttpContext.User.Identity.Name);
            ViewBag.nomeLogado = nomeLogado;

            return View();
        }

        [HttpPost]
        public IActionResult Create(UsuarioViewModel usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome) && !ModelState.IsValid)
            {
                return View();
            }
            if (usuario.UsuarioId == 0)
            {
                var usuarioDomain = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                _usuarioRepository.Add(usuarioDomain);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var nomeLogado = _usuarioRepository.GetByNameUser(HttpContext.User.Identity.Name);
            ViewBag.nomeLogado = nomeLogado;

            var usuario = _usuarioRepository.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
            return View(usuarioViewModel);
        }

        [HttpPost]
        public IActionResult Edit(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioDomain = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                _usuarioRepository.Update(usuarioDomain);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        
        [HttpDelete]
        public void Delete(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            _usuarioRepository.Remove(usuario);
        }
    }
}