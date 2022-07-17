using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; // Reconhece o uso do List

namespace FilmesAPI.Controllers
{
    // Definindo a classe como um controlador
    [ApiController]
    // Explicitando qual a rota do controlador
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        // Criando lista de filmes para "salvar"
        private static List<Filme> filmes = new List<Filme>();

        // Definindo o verbo HTTP como "salvar", para identificar o uso do nosso método
        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme) // "FromBody" indica que esperamos o parametro através do corpo da requisição
        {
            // Guardando os filmes atraves de uma lista
            filmes.Add(filme);

            // Validando o filme que estamos recebendo
            Console.WriteLine(filme.Titulo);
        }
    }
}
