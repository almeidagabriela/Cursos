using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data 
{
    /* DbContext: Explicita que a nossa classe é um contexto 
    onde há comunicação entre o banco de dados e a API */
    public class AppDbContext : DbContext
    {
        // Construtor onde passamos nosso parametro de opções (opt) para o construtor base do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt)
        {

        }

        // Explicitar que no momento da criação realizaremos algumas definições personalizadas
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            // Definições da Entidade do tipo Endereço
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema) // Determinando que um objeto do tipo endereço possui um cinema
                .WithOne(cinema => cinema.Endereco) // O cinema possui um endereço
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId); // Indicando que a chave estrangeira está na classe cinema pelo campo EnderecoId
        }

        // Propriedade utilizando DbSet (responsavel pelo conjunto de dados para acesso ao banco)
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}