using Blog.Data.Context;
using Blog.Data.Entidade;
using Blog.Data.Interface;

namespace Blog.Data.Repositorio
{
    public class AutorRepositorio : Repositorio<Autor>, IAutorRepositorio
    {
        public AutorRepositorio(BlogDbContext context) : base(context)
        {
        }
    }
}
