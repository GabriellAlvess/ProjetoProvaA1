using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjetoProvaA1.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Dados adicionais do perfil do usuário
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Relacionamentos com Reviews e UserGameLibrary
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<UserGameLibrary> Library { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // DbSets para cada entidade
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserGameLibrary> UserGameLibraries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Developer> Developers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Preserve configurações do Identity

            // Remover convenção de pluralização de nomes de tabela
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configuração de relacionamento User - Review (1:N)
            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(false);

            // Configuração de relacionamento Game - Review (1:N)
            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Game)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId)
                .WillCascadeOnDelete(false);

            // Configuração de relacionamento Game - Developer (1:N)
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperId)
                .WillCascadeOnDelete(false);

            // Configuração de relacionamento Game - Genre (N:N)
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Genres)
                .WithMany(gen => gen.Games)
                .Map(m =>
                {
                    m.ToTable("GameGenres"); // Nome da tabela de associação
                    m.MapLeftKey("GameId");
                    m.MapRightKey("GenreId");
                });

            // Configuração de relacionamento User - Game (UserGameLibrary - N:N)
            modelBuilder.Entity<UserGameLibrary>()
                .HasKey(ug => new { ug.UserId, ug.GameId }); // Chave composta para UserGameLibrary

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
        }
    }
}
