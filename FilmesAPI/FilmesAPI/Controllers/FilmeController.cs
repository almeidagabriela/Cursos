using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; // Reconhece o uso do List
using System.Linq;

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
        private static int id = 1; // Identificador de cada filme cadastrado

        // Definindo o verbo HTTP como "salvar", para identificar o uso do nosso método
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme) // "FromBody" indica que esperamos o parametro através do corpo da requisição
        {
            // Incrementando o id para cada cadastro feito
            filme.IdFilme = id++;
            // Guardando os filmes atraves de uma lista
            filmes.Add(filme);

            // Validando o filme que estamos recebendo
            //Console.WriteLine(filme.Titulo);

            // CreateAtAction: mostra o status da requisição e onde o recurso foi criado (por meio do Header "Location")
            /* CreateAtAction(
                    nameof(Ação que precisa ser executada para recuperar o recurso),
                    new {Valores que queremos passar na rota referenciada acima},
                    objeto/valor que estamos tratando
                )
            */
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {Id = filme.IdFilme}, filme);
        }

        // Método para recuparar os filmes que foram cadastrados
        /* 
            Utilizando o IEnumerable nós garantimos que se 
            caso mudarmos o retorno (lista de filmes), 
            o método não vai quebrar:
            public IEnumerable<Filme> RecuperaFilmes()
        */
        // GET: Verbo HTTP para consultar informação
        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            // Retorno de status HTTP 200
            return Ok(filmes);
        }

        // Método para recuperar um filme especifico
        // Especificando no verbo HTTP que há recebimento de parametro
        [HttpGet("{id}")]
        // IActionResult é o tipo de retorno que utilizamos para resultados de status HTTP
        public IActionResult RecuperaFilmesPorId(int id)
        {
            // Opção 1

            // // Para cada filme da lista
            // foreach(Filme filme in filmes) 
            // {
            //     // Verifica se o id do filme é igual ao passado por parametro
            //     if(filme.IdFilme == id)
            //     {
            //         return filme;
            //     }
            // }

            // return null;

            // Opção 2
            
            // Caso não encontre o id, o "default" é o retorno nulo
            Filme filme = filmes.FirstOrDefault(filme => filme.IdFilme == id);

            if(filme != null)
            {
                //Retorna status HTTP 200
                return Ok(filme);
            }

            //Caso não tenha encontrado o filme, o retorno será o status HTTP 404
            return NotFound();
        }
    }
}
