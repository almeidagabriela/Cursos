using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FilmesAPI.Authorization 
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinimaRequirement requirement)
        {
            // Se não achar uma claim do tipo "DateOfBirth"
            if(!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            DateTime dataNascimento = Convert.ToDateTime(context.User.FindFirst(c => 
                // Consulta do contexto do usuario a claim do tipo "DateOfBirth" (Data de Nascimento) e retorna o valor
                c.Type == ClaimTypes.DateOfBirth    
            ).Value);

            int idadeObtida = DateTime.Today.Year - dataNascimento.Year;

            // Valida se a pessoa ainda não fez aniversário esse ano
            if(dataNascimento > DateTime.Today.AddYears(-idadeObtida))
                idadeObtida--;

            if(idadeObtida >= requirement.IdadeMinima) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}