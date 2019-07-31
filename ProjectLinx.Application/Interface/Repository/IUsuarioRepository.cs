using ProjectLinx.Domain;
using System.Collections.Generic;

namespace ProjectLinx.Application.Interface.Repository
{
    public interface IUsuarioRepository
    {
        void Add(Usuario user);
        IEnumerable<Usuario> GetAll();
        IEnumerable<Usuario> GetUsuarios(string busca);
        Usuario GetById(int id);
        void Remove(Usuario usuario);
        void Update(Usuario user);
        Usuario GetByEmail(string email);
        string GetByNameUser(string email);
    }
}