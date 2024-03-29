using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public LogoutService(SignInManager<CustomIdentityUser> signInManager)
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