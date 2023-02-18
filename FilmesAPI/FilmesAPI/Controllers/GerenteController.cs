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
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto dto)
        {
            ReadGerenteDto readDTO = _gerenteService.AdicionaGerente(dto);

            return CreatedAtAction(nameof(RecuperaGerentesPorId), new {Id = readDTO.Id}, readDTO);
        }

        [HttpGet]
        public IActionResult RecuperaGerentes()
        {
            List<ReadGerenteDto> readDTO = _gerenteService.RecuperaGerentes();

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentesPorId(int id)
        {
            ReadGerenteDto readDTO = _gerenteService.RecuperaGerentesPorId(id);

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaGerente(int id, [FromBody] UpdateGerenteDto gerenteDto)
        {
            Result resultado = _gerenteService.AtualizaGerente(id, gerenteDto);

            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
            Result resultado = _gerenteService.DeletaGerente(id);

            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}