using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>>  userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDTO)
        {
            // Conversão de um Dto para uma Model
            Usuario usuario = _mapper.Map<Usuario>(createDTO);

            // Conversão da Model para AspNetUsers (tabela gerada pelo Identity)
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            // Criar usuário de maneira assincrona
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDTO.Password);

            if(resultadoIdentity.Result.Succeeded) return Result.Ok();

            return Result.Fail("Falha ao cadastrar usuário");
        }
    }
}