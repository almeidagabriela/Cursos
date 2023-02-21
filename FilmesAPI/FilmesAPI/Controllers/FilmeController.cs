using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
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
        // Referência do serviço de comunicação com o banco
        private FilmeService _filmeService;

        // Inicializando o _context via construtor
        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        // Somente usuário com a Role "admin" tem permissão para adicionar um filme
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDTO) // "FromBody" indica que esperamos o parametro através do corpo da requisição
        {
            ReadFilmeDto readDto = _filmeService.AdicionaFilme(filmeDTO);

            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {Id = readDto.IdFilme}, readDto);
        }

        // GET: Verbo HTTP para consultar informação
        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {   
            List<ReadFilmeDto> readDto =  _filmeService.RecuperaFilmes(classificacaoEtaria);
            
            // Se encontrou algum filme
            if(readDto != null) return Ok(readDto);

            return NotFound();
        }

        // Método para recuperar um filme especifico
        // Especificando no verbo HTTP que há recebimento de parametro
        [HttpGet("{id}")]
        // IActionResult é o tipo de retorno que utilizamos para resultados de status HTTP
        public IActionResult RecuperaFilmesPorId(int id)
        {
            ReadFilmeDto readDto = _filmeService.RecuperaFilmesPorId(id);

            // Se encontrou o filme
            if(readDto != null) return Ok(readDto);
            
            //Caso não tenha encontrado o filme, o retorno será o status HTTP 404
            return NotFound();
        }

        // Verbo para atualizacao de recursos do sistema
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDTO)
        {
            Result resultado = _filmeService.AtualizaFilme(id, filmeDTO);

            // Caso o resultado tenha falhado
            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }

        // Verbo para deletar um recurso do sistema por ID
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Result resultado = _filmeService.DeletaFilme(id);

            // Caso o resultado tenha falhado
            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
