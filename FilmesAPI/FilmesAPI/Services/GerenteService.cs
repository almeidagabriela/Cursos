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
    public class GerenteService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente); // Inclusão
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public ReadGerenteDto RecuperaGerentesPorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if(gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

                return gerenteDto;
            }

            return null;
        }

        public List<ReadGerenteDto> RecuperaGerentes()
        {
            List<Gerente> gerentes = _context.Gerentes.ToList();

            if(gerentes == null) return null;

            return _mapper.Map<List<ReadGerenteDto>>(gerentes);
        }

        public Result AtualizaGerente(int id, UpdateGerenteDto gerenteDto)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if(gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _mapper.Map(gerenteDto, gerente);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if(gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _context.Remove(gerente);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}