﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Livro;
using WebApi.Dto.Vinculo;
using WebApi.Models;
using WebApi.Services.Livros;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livrointerface;
        public LivroController(ILivroInterface livrointerface)
        {
            _livrointerface = livrointerface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livrointerface.ListarLivros();
            return Ok(livros);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livros = await _livrointerface.CriarLivro(livroCriacaoDto);
            return Ok(livros);


        }

        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            var livro = await _livrointerface.BuscarLivroPorIdAutor(idAutor);
            return Ok(livro);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livrointerface.BuscarLivroPorId(idLivro);
            return Ok(livro);
        }

        [HttpDelete("ExcluirLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ExcluirLivro(int idLivro)
        {
            var livro = await _livrointerface.ExcluirLivro(idLivro);
            return Ok(livro);

        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>>EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livros = await  _livrointerface.EditarLivro(livroEdicaoDto);
            return Ok(livros);
        }
    }
}
