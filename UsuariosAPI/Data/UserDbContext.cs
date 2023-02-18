using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsuariosAPI.Data
{
    /* 
        Definindo o que estamos utilizando com o IdentityDbContext
        IdentityUser que possui como identificar um tipo Inteiro
        IdentityRole represetando o papel
        int representando uma chave utilizada para identificação
    */
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {
            
        }
    }
}