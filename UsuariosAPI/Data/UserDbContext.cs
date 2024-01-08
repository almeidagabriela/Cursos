using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Criando um usuário ADMIN fora do fluxo de cadastro
            IdentityUser<int> admin = new IdentityUser<int> 
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(), // Identificador unico
                Id = 99999 // O numero foi definido como um valor que temos certeza que não temos na base
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

            admin.PasswordHash = hasher.HashPassword(
                admin, 
                _configuration.GetValue<string>("admininfo:password")
            );

            builder.Entity<IdentityUser<int>>().HasData(admin);

            // Criando Roles
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> 
                {
                    Id = 99999,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            );

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> 
                {
                    Id = 99998,
                    Name = "regular",
                    NormalizedName = "REGULAR"
                }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> 
                {
                    RoleId = 99999,
                    UserId = 99999
                }
            );
        }
    }
}