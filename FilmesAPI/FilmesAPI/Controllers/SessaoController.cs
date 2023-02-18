using System.Linq;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto dto)
        {
            ReadSessaoDto readDTO = _sessaoService.AdicionaSessao(dto);

            return CreatedAtAction(nameof(RecuperaSessoesPorId), new {Id = readDTO.Id}, readDTO);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessoesPorId(int id)
        {
            ReadSessaoDto readDTO = _sessaoService.RecuperaSessoesPorId(id);

            if(readDTO != null) return Ok(readDTO);

            return NotFound();
        }
    }
}