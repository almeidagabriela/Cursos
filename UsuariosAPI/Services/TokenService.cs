using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        // Responsável pela criação do Token
        public Token CreateToken(IdentityUser<int> usuario)
        {
            // Receber exigências do usuário
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            // Gerar chave para criptografia
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajs09asjd09sajcnzxn")
            );

            // Gerar credenciais a partir da chave e um algoritmo de criptografia
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            // Gerando o token
            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais, // definindo partes de segurança
                expires: DateTime.UtcNow.AddHours(1) // tempo de expiração do token: 1 hora
            );

            // Colocando o token em uma string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}