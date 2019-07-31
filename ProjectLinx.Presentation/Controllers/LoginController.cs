using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Domain;

namespace ProjectLinx.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login usuarioLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!IsUserAuthenticated(usuarioLogin.Email, usuarioLogin.Senha))
            {
                ModelState.AddModelError(nameof(Login.Email), "Usuário ou senha inválidos!");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioLogin.Email)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login/Index");
        }

        private bool IsUserAuthenticated(string email, string senha)
        {
            var usuarioLogin = _usuarioRepository.GetByEmail(email);

            if (usuarioLogin == null)
                return false;

            if (usuarioLogin.Senha == senha)
                return true;

            return false;
        }
    }
}