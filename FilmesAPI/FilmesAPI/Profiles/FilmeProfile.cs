using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    // Classe para armazenamento de perfis
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            // Mapper De: CreateFilmeDTO Para: Filme
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
            CreateMap<UpdateFilmeDto, Filme>();
        }
    }
}