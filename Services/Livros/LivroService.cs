using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto.Livro;
using WebApi.Dto.Vinculo;
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
        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livros = await _context.Livros.Include(a => a.Autor)
                    .Where(livroBanco => livroBanco.Autor.Id == idAutor).ToListAsync();

                if(livros is null)
                {
                    resposta.Mensagem = "Autor não localizado!";
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Consulta realizada com Sucesso";
                return resposta;

            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();

            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(a => a.Id == idLivro);
                if(livro is null)
                {
                   resposta.Mensagem = "livro não localizado";
                    return resposta;
                    
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado!";
                return resposta; 
            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
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

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros.
                    Include(a=>a.Autor)
                    .FirstOrDefaultAsync(a => a.Id == livroEdicaoDto.Id);

               // var autor = await _context.Autores.
                  //  FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);
                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado";
                    return resposta;
                }

               /* if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado";
                    return resposta;
                } */




                livro.Titulo = livroEdicaoDto.Titulo;
                //livro.Autor = autor;
                    

             



                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                return resposta;
            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livros = await _context.Livros.Include(a=>a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if(livros is null)
                {
                    resposta.Mensagem = "Houve um erro ao tentar excluir";
                    return resposta;
                }

                _context.Remove(livros);
                await _context.SaveChangesAsync();  

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro excluido com sucesso";
                return resposta;
            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
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
