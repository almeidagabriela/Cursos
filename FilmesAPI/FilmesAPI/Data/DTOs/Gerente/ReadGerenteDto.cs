using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmesAPI.Models;

namespace FilmesAPI.Data.DTOs
{
    public class ReadGerenteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // Definindo "Cinemas" como um objeto anonimo
        public object Cinemas {get; set; }
    }
}