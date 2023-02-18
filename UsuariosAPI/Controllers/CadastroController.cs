using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDTO)
        {
            // TODO: chamar o service
            return Ok();
        }
    }
}