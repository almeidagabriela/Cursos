using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data 
{
    /* DbContext: Explicita que a nossa classe é um contexto 
    onde há comunicação entre o banco de dados e a API */
    public class FilmeContext : DbContext
    {
        // Construtor onde passamos nosso parametro de opções (opt) para o construtor base do DbContext
        public FilmeContext(DbContextOptions<FilmeContext> opt) : base (opt)
        {

        }

        // Propriedade utilizando DbSet (responsavel pelo conjunto de dados para acesso ao banco)
        public DbSet<Filme> Filmes { get; set; }
    }
}