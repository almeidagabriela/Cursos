using FilmesAPI.Models;

namespace FilmesAPI.Data.DTOs
{
    public class ReadSessaoDto
    {
        public int Id { get; set; }
        public Cinema Cinema { get; set; }
        public Filme Filme { get; set; }
    }
}