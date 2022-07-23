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
        public void AdicionaFilme([FromBody] Filme filme) // "FromBody" indica que esperamos o parametro através do corpo da requisição
        {
            // Incrementando o id para cada cadastro feito
            filme.IdFilme = id++;
            // Guardando os filmes atraves de uma lista
            filmes.Add(filme);

            // Validando o filme que estamos recebendo
            Console.WriteLine(filme.Titulo);
        }

        // Método para recuparar os filmes que foram cadastrados
        // Utilizando o IEnumerable nós garantimos que se caso mudarmos o retorno (lista de filmes), o método não vai quebrar
        // GET: Verbo HTTP para consultar informação
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return filmes;
        }

        // Método para recuperar um filme especifico
        // Especificando no verbo HTTP que há recebimento de parametro
        [HttpGet("{id}")]
        public Filme RecuperaFilmesPorId(int id)
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
            
            // Caso não encontre o id, o "default" é o retorno nulo com o status HTTP 204 No Content
            return filmes.FirstOrDefault(filme => filme.IdFilme == id);
        }
    }
}
