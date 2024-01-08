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
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService = null)
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
                Token token = _tokenService
                    .CreateToken(
                        identityUser,
                        _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault() // Inclui informação da Role do usuário
                    );

                return Result.Ok().WithSuccess(token.Value); // Retorno OK com token
            }

            return Result.Fail("Login falhou");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);
            
            if(identityUser != null)
            {
                // Gera um token para redefinição de senha
                string codigoDeRecuperacao = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");
        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);

            // Realizando operação de reset de senha
            IdentityResult resultadoIdentity = _signInManager
                .UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

            if (resultadoIdentity.Succeeded) return Result.Ok().WithSuccess("Senha alterada com sucesso!");

            return Result.Fail("Houve um erro na operação.");
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            // Recuperando usuário a partir do email
            return _signInManager
                .UserManager
                    .Users
                        .FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
        }
    }
}