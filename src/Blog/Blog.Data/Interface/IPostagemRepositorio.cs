using Blog.Data.Entidade;

namespace Blog.Data.Interface
{
    public interface IPostagemRepositorio: IRepositorio<Postagem>
    {
        Task<IEnumerable<Postagem>> ObterTodas();
        Task<Postagem> ObterPorId(int id);
    }
}
