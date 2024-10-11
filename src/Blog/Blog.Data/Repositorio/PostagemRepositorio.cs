
using Blog.Data.Context;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositorio
{
    public class PostagemRepositorio : Repositorio<Postagem>, IPostagemRepositorio
    {
        public PostagemRepositorio(BlogDbContext context) : base(context)
        {
        }

        public Task<Postagem> ObterPorId(int id)
        {
            return _context.Postagens
                .Include(p => p.Autor)
                .Include(p => p.Comentarios)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Postagem>> ObterTodas()
        {
          return await _context.Postagens
                .Include(p => p.Autor)
                .Include(p => p.Comentarios)
                .ToListAsync();
        }
    }
}
