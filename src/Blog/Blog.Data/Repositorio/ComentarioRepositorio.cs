using Blog.Data.Context;
using Blog.Data.Entidade;
using Blog.Data.Interface;

namespace Blog.Data.Repositorio
{
    public class ComentarioRepositorio : Repositorio<Comentario>, IComentarioRepositorio
    {
        public ComentarioRepositorio(BlogDbContext context) : base(context)
        {
        }
    }
}
