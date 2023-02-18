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
    public class EnderecoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AdicionarEndereco(CreateEnderecoDto enderecoDTO)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public List<ReadEnderecoDto> RecuperaEnderecos()
        {
            List<Endereco> enderecos = _context.Enderecos.ToList();

            if(enderecos == null) return null;

            return _mapper.Map<List<ReadEnderecoDto>>(enderecos);
        }

        public ReadEnderecoDto RecuperaEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            
            if(endereco != null)
            {
                ReadEnderecoDto enderecoDTO = _mapper.Map<ReadEnderecoDto>(endereco);
                return enderecoDTO;
            }

            return null;
        }

        public Result AtualizaEndereco(int id, UpdateEnderecoDto enderecoDTO)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            
            if(endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(enderecoDTO, endereco);
            _context.SaveChanges();

            return Result.Ok();
        }

        internal Result DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            
            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _context.Remove(endereco);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}