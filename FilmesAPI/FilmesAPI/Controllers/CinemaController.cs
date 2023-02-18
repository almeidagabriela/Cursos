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
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
  
        // Novo cinema
        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDTO)
        {
            ReadCinemaDto readDTO = _cinemaService.AdicionaCinema(cinemaDTO);

            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = readDTO.Id }, readDTO);
        }

        // Get All
        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            List<ReadCinemaDto> readDTO = _cinemaService.RecuperaCinemas(nomeDoFilme);

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }

        // Get One
        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            ReadCinemaDto readDTO = _cinemaService.RecuperaCinemasPorId(id);

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }

        // Atualiza o cinema
        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result resultado = _cinemaService.AtualizaCinema(id, cinemaDto);

            // Caso o resultado tenha falhado
            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }

        // Deleta o cinema
        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result resultado = _cinemaService.DeletaCinema(id);

            // Caso o resultado tenha falhado
            if(resultado.IsFailed) return NotFound();

            return NoContent();
        }

    }
}