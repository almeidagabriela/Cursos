using System;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Requests;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        // Gerenciador de login
        private SignInManager<IdentityUser<int>> _signInManager;

        public LoginService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LogarUsuario(LoginRequest request)
        {
            // O gerenciador de login tenta realizar a autenticação
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(
                    request.Username, 
                    request.Password,
                    false, // Se o cookie de login deve persistir depois do navegador ser fechado
                    false); // Bloquear conta do usuário caso haja falha no login

            if(resultadoIdentity.Result.Succeeded) return Result.Ok();

            return Result.Fail("Login falhou");
        }
    }
}