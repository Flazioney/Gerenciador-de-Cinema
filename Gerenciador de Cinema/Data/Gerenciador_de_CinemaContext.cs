using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Cinema.Data
{
    public class Gerenciador_de_CinemaContext : DbContext
    {
        public Gerenciador_de_CinemaContext(DbContextOptions<Gerenciador_de_CinemaContext> options)
            : base(options)
        {
        }

        public DbSet<Gerenciador_de_Cinema.Models.Filmes> Filmes { get; set; }

        public DbSet<Gerenciador_de_Cinema.Models.Salas> Salas { get; set; }

        public DbSet<Gerenciador_de_Cinema.Models.Sessao> Sessao { get; set; }

        public DbSet<Gerenciador_de_Cinema.Models.Login> Login { get; set; }

        
    }
}
