using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    /* 
        DTOs são responsaveis por transferir os dados entre as partes do sistema, 
        podemos indicar o que queremos receber e o que queremos retornar
        
        Nesse caso indicamos o que retornaremos
    */
    public class ReadFilmeDto
    {
        [Key]
        [Required]
        public int IdFilme { get; set; }
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo diretor é obrigatório.")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O gênero não pode passar de 30 caracteres.")]
        public string Genero { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 e no máximo 600 minutos.")]
        public int Duracao { get; set; }
        public DateTime HoraDaConsulta { get; set; }
    }
}