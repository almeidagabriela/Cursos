using System;
using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Models
{
    // Classe customizada que estende do IdentityUser
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
    }
}