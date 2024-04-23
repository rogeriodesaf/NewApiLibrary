using WebApi.Models;

namespace WebApi.Services
{
    public interface IAutorInterface
    {
        
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);

        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);  
    }
}
