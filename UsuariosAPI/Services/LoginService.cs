using System;
using System.Linq;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        // Gerenciador de login
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService = null)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
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

            if(resultadoIdentity.Result.Succeeded)
            {
                // Recuperando usuario
                var identityUser = _signInManager
                    .UserManager
                        .Users
                            .FirstOrDefault(usuario => 
                                usuario.NormalizedUserName == request.Username.ToUpper());

                // Gerando token
                Token token = _tokenService.CreateToken(identityUser);

                return Result.Ok().WithSuccess(token.Value); // Retorno OK com token
            }

            return Result.Fail("Login falhou");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            // Recuperando usuário a partir do email
            IdentityUser<int> identityUser = _signInManager
                .UserManager
                    .Users
                        .FirstOrDefault(user => user.NormalizedEmail == request.Email.ToUpper());
            
            if(identityUser != null)
            {
                // Gera um token para redefinição de senha
                string codigoDeRecuperacao = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");
        }
    }
}