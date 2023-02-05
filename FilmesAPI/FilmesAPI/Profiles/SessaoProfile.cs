using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            // Mapper <De, Para>
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                // Calculando o horário de inicio da sessão em tempo de execução
                .ForMember(dto => dto.HorarioDeInicio, opts => opts
                    // Subtraindo a duração do horário de encerramento, obtendo então o horário de inicio.
                    .MapFrom(dto => dto.HorarioDeEncerramento.AddMinutes(dto.Filme.Duracao*(-1)))
                );
        }
    }
}