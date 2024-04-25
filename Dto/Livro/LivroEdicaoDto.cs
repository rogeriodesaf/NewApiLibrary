﻿using System.Text.Json.Serialization;
using WebApi.Dto.Vinculo;
using WebApi.Models;

namespace WebApi.Dto.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

      
        public AutorVinculoDto Autor { get; set; }
    }
}
