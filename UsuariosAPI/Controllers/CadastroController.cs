using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDTO)
        {
            Result resultado = _cadastroService.CadastraUsuario(createDTO);

            if(resultado.IsFailed) return StatusCode(500);

            return Ok(resultado.Successes);
        }

        [HttpPost("/ativar")]
        public IActionResult AtivarContaUsuario(AtivarContaRequest request)
        {
            Result resultado = _cadastroService.AtivarContaUsuario(request);

            if(resultado.IsFailed) return StatusCode(500);

            return Ok(resultado.Successes);
        }
    }
}