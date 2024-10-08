
using Blog.Data.Context;
using Blog.Data.Entidade;
using Blog.Data.Interface;

namespace Blog.Data.Repositorio
{
    public class PostagemRepositorio : Repositorio<Postagem>, IPostagemRepositorio
    {
        public PostagemRepositorio(BlogDbContext context) : base(context)
        {
        }
    }
}
