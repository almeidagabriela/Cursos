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
    public class FilmeService
    {
        /* Utilizando o context criado para comunicação com o banco
            Podemos utiliza-lo para acessar, guardar e recuperar informações no banco
        */
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDTO)
        {
            // Converte o filmeDTO em um filme utilizando o AutoMapper
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            // Adicionando o filme no banco
            _context.Filmes.Add(filme);
            // Informando que quer realmente executar e salvar a informação no banco
            _context.SaveChanges();

            // Realizando um automapper a partir do filme que foi criado
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> RecuperaFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;

            if(classificacaoEtaria == null)
            {
                // Retorna a lista completa de filmes
                filmes = _context.Filmes.ToList();
            }
            else
            {
                // Filtrando filmes com classificação etária menor ou igual ao que foi passado no queryParameter
                filmes = _context
                .Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }

            if(filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readDto;
            }

            return null;
        }

        public ReadFilmeDto RecuperaFilmesPorId(int id)
        {
            // Caso não encontre o id, o "default" é o retorno nulo
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            if(filme != null)
            {
                // Converte o filme para um DTO com o AutoMapper
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                //Retorna dados do filme
                return filmeDto;
            }

            return null;
        }

        public Result AtualizaFilme(int id, UpdateFilmeDto filmeDTO)
        {
            // Recuperando os dados do filme pelo ID
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            // Caso não encontre o filme
            if(filme == null)
            {
                // Utilizando o FluentResults para customizar o retorno
                return Result.Fail("Filme não encontrado");
            }

            // Sobrescreve o filme com as informações do filmeDTO
            _mapper.Map(filmeDTO, filme);

            // Salvar mudanças
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaFilme(int id)
        {
            // Recuperando os dados do filme pelo ID
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.IdFilme == id);

            // Caso não encontre o filme
            if(filme == null)
            {
                // Utilizando o FluentResults para customizar o retorno
                return Result.Fail("Filme não encontrado");
            }

            // Removendo filme
            _context.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}