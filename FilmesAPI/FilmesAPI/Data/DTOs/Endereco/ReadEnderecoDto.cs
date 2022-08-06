using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    /* 
        DTOs são responsaveis por transferir os dados entre as partes do sistema, 
        podemos indicar o que queremos receber e o que queremos retornar
        
        Nesse caso indicamos o que retornaremos
    */
    public class ReadEnderecoDto
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public DateTime HoraDaConsulta { get; set; }
    }
}