using System;
using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Models
{
    // Classe customizada que extende do IdentityUser
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
    }
}