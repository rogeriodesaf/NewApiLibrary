using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto.Autor;
using WebApi.Models;

namespace WebApi.Services
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context; 
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
           ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == idAutor);
              
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor encontrado";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor encontrado";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
           ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();  
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).
                    FirstOrDefaultAsync(x => x.Id == idLivro);


                if (livro == null)
                {
                    resposta.Mensagem = "Autor não localizado";
                    return resposta;
                }
                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor encontrado com sucesso";
                return resposta;
            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = new AutorModel();
                {
                    autor.Nome = autorCriacaoDto.Nome;
                    autor.Sobrenome = autorCriacaoDto.Sobrenome;
                };

                _context.Add(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor Criado com sucesso";
                return resposta;

            }
            catch(Exception ex)
            {
                resposta.Mensagem=ex.Message;
                resposta.Status = false;
                return resposta;
            }


        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>(); 
            try
            {
                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
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
