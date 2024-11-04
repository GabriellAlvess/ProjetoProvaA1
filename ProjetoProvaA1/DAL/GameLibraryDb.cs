using ProjetoProvaA1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameLibraryApp.Models
{
    // AppDbContext agora herda de IdentityDbContext<ApplicationUser> para incluir suporte ao Identity
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() : base("AppDbContext") // Nome da conexão no Web.config
        {
           

        }

        // DbSets para cada entidade

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<UserGameLibrary> UserGameLibraries { get; set; }

        // Método Create para facilitar a criação do contexto
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }


    }
}
