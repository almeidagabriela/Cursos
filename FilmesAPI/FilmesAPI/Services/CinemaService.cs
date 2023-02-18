using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services 
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDTO)
        {
            /// Converte cinemaDTO em cinema
            Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            // Realizando um automapper a partir do cinema que foi criado
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();

            if(cinemas == null)
            {
                return null;
            }

            if(!string.IsNullOrEmpty(nomeDoFilme))
            {
                // A partir de um cinema qualquer, que está na lista de cinemas, realizar uma busca por filtro
                IEnumerable<Cinema> query = from cinema in cinemas 
                        where cinema.Sessoes.Any(sessao => // qualquer sessão que este cinema tenha
                            sessao.Filme.Titulo == nomeDoFilme) // o titulo do filme é igual ao que está sendo procurado via query parameter
                        select cinema;

                cinemas = query.ToList();
            }

            return _mapper.Map<List<ReadCinemaDto>>(cinemas);
        }

        public ReadCinemaDto RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            
            if(cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return cinemaDto;
            }

            return null;
        }

        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDTO)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinema == null)
            {
                // Utilizando o FluentResults para customizar o retorno
                return Result.Fail("Cinema não encontrado");
            }

            _mapper.Map(cinemaDTO, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinema == null)
            {
                // Utilizando o FluentResults para customizar o retorno
                return Result.Fail("Cinema não encontrado");
            }

            _context.Remove(cinema);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}