using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Gerente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        // Um gerente pode exercer seu cargo em nenhum, um ou muitos cinemas, por isso utilizamos um List
        public virtual List<Cinema> Cinemas { get; set; }
    }
}