using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }

        // virtual: explicíta que a propriedade é Lazy
        [JsonIgnore] // Ao consultar um endereço ele vai ignorar a propriedade Cinema
        public virtual Cinema Cinema { get; set; }
    }
}