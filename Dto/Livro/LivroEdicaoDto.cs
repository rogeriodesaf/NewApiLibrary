﻿using WebApi.Models;

namespace WebApi.Dto.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }


        public AutorModel Autor { get; set; }
    }
}
