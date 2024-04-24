using WebApi.Dto.Livro;
using WebApi.Models;

namespace WebApi.Services.Livros
{
    public interface ILivroInterface
    {
       Task<ResponseModel<List<LivroModel>>> ListarLivros();

        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorId(int idLivro);

        Task<ResponseModel<List<LivroModel>>> BuscarAutorLivroPorId(int autorId);

        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);

        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);

        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
    }
}
