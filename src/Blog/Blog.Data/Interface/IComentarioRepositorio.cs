using Blog.Data.Entidade;

namespace Blog.Data.Interface
{
    public interface IComentarioRepositorio:IRepositorio<Comentario>
    {
        Task<Comentario> ObterPorId(int id);
    }
}
