using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDTO)
        {
            // Conversão de um Dto para uma Model
            Usuario usuario = _mapper.Map<Usuario>(createDTO);

            // Conversão da Model para AspNetUsers (tabela gerada pelo Identity)
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            // Criar usuário de maneira assincrona
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDTO.Password);

            if(resultadoIdentity.Result.Succeeded)
            {
                // Gerar token de ativação de conta utilizando o Identity
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;

                // Enviar email de confirmação ao usuário
                _emailService.EnviarEmail(
                    new[] { usuarioIdentity.Email }, // Lista de destinatarios
                    "Ativação de conta", // Assunto
                    usuarioIdentity.Id,
                    code
                );

                return Result.Ok().WithSuccess(code);
            }

            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result AtivarContaUsuario(AtivarContaRequest request)
        {
            // Recuperando o identityUser
            var identityUser = _userManager
                .Users
                    .FirstOrDefault(usuario => usuario.Id == request.UsuarioId);
                
            // Realiza confirmação de email
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;

            if(identityResult.Succeeded) return Result.Ok();

            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}