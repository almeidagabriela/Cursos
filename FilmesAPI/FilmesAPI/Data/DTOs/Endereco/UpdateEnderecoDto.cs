using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    /* 
        DTOs são responsaveis por transferir os dados entre as partes do sistema, 
        podemos indicar o que queremos receber e o que queremos retornar
        
        Nesse caso indicamos o que esperamos receber
    */
    public class UpdateEnderecoDto
    {
        [Required(ErrorMessage = "O campo logradouro é obrigatório.")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo bairro é obrigatório.")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo número é obrigatório.")]
        public int Numero { get; set; }
    }
}