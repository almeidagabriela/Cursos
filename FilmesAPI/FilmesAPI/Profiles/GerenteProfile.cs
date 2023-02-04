using System.Linq;
using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    // Classe para armazenamento de perfis
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            // Mapper <De, Para>
            CreateMap<CreateGerenteDto, Gerente>();

            // Mapeando de Gerente para ReadGerenteDto
            CreateMap<Gerente, ReadGerenteDto>()
                // Para a propriedade Cinemas, estamos definindo opções de mapeamento
                .ForMember(gerente => gerente.Cinemas, opts => opts
                    // Selecione apenas o Id, Nome, Endereco e o EnderecoId e armazene no objeto anonimo
                    .MapFrom(gerente => gerente.Cinemas.Select
                        (c => new {c.Id, c.Nome, c.Endereco, c.EnderecoId})
                    )
                );
        }
    }
}