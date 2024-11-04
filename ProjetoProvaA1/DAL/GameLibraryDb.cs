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
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Game)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Genres)
                .WithMany(gen => gen.Games)
                .Map(m =>
                {
                    m.ToTable("GameGenres");
                    m.MapLeftKey("GameId");
                    m.MapRightKey("GenreId");
                });

            // Configurando chave composta para UserGameLibrary
            modelBuilder.Entity<UserGameLibrary>()
                .HasKey(ug => new { ug.UserId, ug.GameId });

            modelBuilder.Entity<UserGameLibrary>()
                .HasRequired(ug => ug.User)
                .WithMany(u => u.Library)
                .HasForeignKey(ug => ug.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGameLibrary>()
                .HasRequired(ug => ug.Game)
                .WithMany()
                .HasForeignKey(ug => ug.GameId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }


    }
}
