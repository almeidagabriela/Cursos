using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result DeslogarUsuario()
        {
            // Realiza tarefa assincrona para deslogar usuário atual da aplicação
            var resultadoIdentity = _signInManager.SignOutAsync();

            if(resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();

            return Result.Fail("Logout falhou");
        }
    }
}