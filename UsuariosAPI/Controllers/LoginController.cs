using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result resultado = _loginService.LogarUsuario(request);

            // Se a requisição falhou, o login não foi autorizado
            if(resultado.IsFailed) return Unauthorized();

            return Ok();
        }
    }
}