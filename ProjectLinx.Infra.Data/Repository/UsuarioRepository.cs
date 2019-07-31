using Microsoft.AspNetCore.Http;
using ProjectLinx.Application.Interface.Repository;
using ProjectLinx.Domain;
using ProjectLinx.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace ProjectLinx.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDbContext _usuarioDbContext;
        private readonly IHttpContextAccessor _accessor;

        public UsuarioRepository(UsuarioDbContext usuarioDbContext)
        {
            _usuarioDbContext = usuarioDbContext;
        }

        public void Add(Usuario usuario)
        {
            _usuarioDbContext.Set<Usuario>().Add(usuario);
            _usuarioDbContext.SaveChanges();
        }

        public Usuario GetById(int id)
        {
            return _usuarioDbContext.Set<Usuario>().Find(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _usuarioDbContext.Usuario.ToList();
        }

        public IEnumerable<Usuario> GetUsuarios(string busca)
        {
            if (!string.IsNullOrEmpty(busca))
            {
                return _usuarioDbContext.Usuario.Where(u => u.Nome.Contains(busca)).ToList();
            }
            return _usuarioDbContext.Usuario.ToList();
        }

        public void Remove(Usuario usuario)
        {
            _usuarioDbContext.Set<Usuario>().Remove(usuario);
            _usuarioDbContext.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _usuarioDbContext.Usuario.Update(usuario);
            _usuarioDbContext.SaveChanges();
        }

        public Usuario GetByEmail(string email)
        {
            return _usuarioDbContext.Usuario.FirstOrDefault(x => x.Email.ToLower() == email.ToLower().Trim());
        }

        public string GetByNameUser(string email)
        {
            return _usuarioDbContext.Usuario.FirstOrDefault(u => u.Email.Equals(email)).Nome;
        }
    }
}