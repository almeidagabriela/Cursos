using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        // Informações do Filme

        [Key] // Identifica a chave primaria
        [Required]
        public int IdFilme { get; set; }
        // Required: Validação de campo obrigatório com mensagem de erro especifica
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo diretor é obrigatório.")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O gênero não pode passar de 30 caracteres.")] // Definindo a quantidade máxima de caracteres
        public string Genero { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 e no máximo 600 minutos.")] // Validação de tamanho da duração com mensagem de erro especifica
        public int Duracao { get; set; }
        public virtual List<Sessao> Sessoes { get; set; }
    }
}
