using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        // Create
        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDTO)
        {
            ReadEnderecoDto readDTO = _enderecoService.AdicionarEndereco(enderecoDTO);

            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = readDTO.Id}, readDTO);
        }

        // Get All
        [HttpGet]
        public IActionResult RecuperaEnderecos()
        {
            List<ReadEnderecoDto> readDTO = _enderecoService.RecuperaEnderecos();

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }

        // Get One
        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            ReadEnderecoDto readDTO = _enderecoService.RecuperaEnderecosPorId(id);

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDTO)
        {
            Result resultado = _enderecoService.AtualizaEndereco(id, enderecoDTO);

            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Result resultado = _enderecoService.DeletaEndereco(id);

            // Caso o resultado tenha falhado
            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}