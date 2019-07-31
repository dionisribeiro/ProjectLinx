using Microsoft.EntityFrameworkCore;
using ProjectLinx.Domain;
using System;

namespace ProjectLinx.Infra.Data.Context
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options)
        : base(options)
        {

        }
        public DbSet<Usuario> Usuario { get; set; }

        internal Usuario FirstOrDefault(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}