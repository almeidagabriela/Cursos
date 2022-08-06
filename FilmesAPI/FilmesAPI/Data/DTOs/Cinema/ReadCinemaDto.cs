using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    /* 
        DTOs são responsaveis por transferir os dados entre as partes do sistema, 
        podemos indicar o que queremos receber e o que queremos retornar
        
        Nesse caso indicamos o que retornaremos
    */
    public class ReadCinemaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        public object Endereco { get; set; }
    }
}