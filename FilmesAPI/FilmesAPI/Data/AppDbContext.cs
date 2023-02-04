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
        
            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas) // O gerente pode ter de zero a muitos cinemas
                .HasForeignKey(cinema => cinema.GerenteId);

            // Criando um relacionamento n para n (filmes e cinemas)
            #region Relacionando Filmes e Cinemas

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(filme => filme.Sessoes)
                .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);

            #endregion
        }

        // Propriedade utilizando DbSet (responsavel pelo conjunto de dados para acesso ao banco)
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
    }
}