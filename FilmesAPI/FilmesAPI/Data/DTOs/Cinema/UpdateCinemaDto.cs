using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    /* 
        DTOs são responsaveis por transferir os dados entre as partes do sistema, 
        podemos indicar o que queremos receber e o que queremos retornar
        
        Nesse caso indicamos o que esperamos receber
    */
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
    }
}