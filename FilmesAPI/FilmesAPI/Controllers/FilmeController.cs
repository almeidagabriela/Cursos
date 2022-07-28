using FilmesAPI.Data;
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
        /* Utilizando o context criado para comunicação com o banco
            Podemos utiliza-lo para acessar, guardar e recuperar informações no banco
        */
        private FilmeContext _context;

        // Inicializando o _context via construtor
        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        // Definindo o verbo HTTP como "salvar", para identificar o uso do nosso método
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme) // "FromBody" indica que esperamos o parametro através do corpo da requisição
        {
            // TESTE: Validando o filme que estamos recebendo
            //Console.WriteLine(filme.Titulo);

            // Adicionando o filme no banco
            _context.Filmes.Add(filme);
            // Informando que quer realmente executar e salvar a informação no banco
            _context.SaveChanges();

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
        public IEnumerable<Filme> RecuperaFilmes()
        {
            // _context.Filmes acessa todo o conjunto de dados da tabela Filmes
            return _context.Filmes;
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
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            if(filme != null)
            {
                //Retorna status HTTP 200
                return Ok(filme);
            }

            //Caso não tenha encontrado o filme, o retorno será o status HTTP 404
            return NotFound();
        }

        // Verbo para atualizacao de recursos do sistema
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] Filme novoFilme)
        {
            // Recuperando os dados do filme pelo ID
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            // Caso não encontre o filme
            if(filme == null)
            {
                return NotFound(); // Retorna um "não encontrado"
            }

            // Atualização campo a campo (não é a melhor forma)
            filme.Titulo = novoFilme.Titulo;
            filme.Diretor = novoFilme.Diretor;
            filme.Genero = novoFilme.Genero;
            filme.Duracao = novoFilme.Duracao;

            // Salvar mudanças
            _context.SaveChanges();
            // Ao realizar o retorno de um put, não retornamos nenhum conteúdo
            return NoContent();
        }

        // Verbo para deletar um recurso do sistema por ID
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            // Recuperando os dados do filme pelo ID
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            // Caso não encontre o filme
            if(filme == null)
            {
                return NotFound(); // Retorna um "não encontrado"
            }

            // Removendo filme
            _context.Remove(filme);
            _context.SaveChanges();

            // Ao realizar o retorno de um delete, não retornamos nenhum conteúdo
            return NoContent();
        }
    }
}
