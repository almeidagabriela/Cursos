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
            CreateMap<Gerente, ReadGerenteDto>();
        }
    }
}