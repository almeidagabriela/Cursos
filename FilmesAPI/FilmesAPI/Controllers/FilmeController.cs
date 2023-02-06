using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        private AppDbContext _context;
        private IMapper _mapper;

        // Inicializando o _context via construtor
        public FilmeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Definindo o verbo HTTP como "salvar", para identificar o uso do nosso método
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDTO) // "FromBody" indica que esperamos o parametro através do corpo da requisição
        {
            // TESTE: Validando o filme que estamos recebendo
            //Console.WriteLine(filme.Titulo);

            // Converte o filmeDTO em um filme utilizando o AutoMapper
            Filme filme = _mapper.Map<Filme>(filmeDTO);

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

        // int? nomeDaVariavel = null - estamos informando com o "?" que o campo é nulável, ou seja, tem a possíbilidade de ser nulo 
        // GET: Verbo HTTP para consultar informação
        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {   
            List<Filme> filmes;

            if(classificacaoEtaria == null)
            {
                // Retorna a lista completa de filmes
                filmes = _context.Filmes.ToList();
            }
            else
            {
                // Filtrando filmes com classificação etária menor ou igual ao que foi passado no queryParameter
                filmes = _context
                .Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }

            if(filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return Ok(readDto);
            }

            return NotFound();
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
                // Converte o filme para um DTO com o AutoMapper
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                //Retorna dados do filme com status HTTP 200
                return Ok(filmeDto);
            }

            //Caso não tenha encontrado o filme, o retorno será o status HTTP 404
            return NotFound();
        }

        // Verbo para atualizacao de recursos do sistema
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDTO)
        {
            // Recuperando os dados do filme pelo ID
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            // Caso não encontre o filme
            if(filme == null)
            {
                // Retorna um "não encontrado"
                return NotFound();
            }

            // Sobrescreve o filme com as informações do filmeDTO
            _mapper.Map(filmeDTO, filme);

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
