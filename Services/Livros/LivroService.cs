using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto.Livro;
using WebApi.Models;

namespace WebApi.Services.Livros
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;
        public LivroService(AppDbContext context)
        {
            _context = context;
        }
        public Task<ResponseModel<List<LivroModel>>> BuscarAutorLivroPorId(int autorId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(a => a.Id == livroCriacaoDto.Autor.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado";
                    return resposta;
                }

                var livro = new LivroModel();
                {
                    livro.Titulo = livroCriacaoDto.Titulo;
                    livro.Autor = autor;

                  };

                

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a=>a.Autor).ToListAsync();
                resposta.Mensagem = "Livro adicionado com sucesso";
                return resposta;

              }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros.Include(a=>a.Autor).ToListAsync();

                if(livros == null)
                {
                    resposta.Mensagem = "Nenhum Livro Encontrado!";
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os autores coletados";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
