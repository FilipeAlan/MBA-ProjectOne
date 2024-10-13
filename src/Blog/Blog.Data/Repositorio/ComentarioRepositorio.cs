using Blog.Data.Context;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositorio
{
    public class ComentarioRepositorio : Repositorio<Comentario>, IComentarioRepositorio
    {
        public ComentarioRepositorio(BlogDbContext context) : base(context)
        {
        }

        public async Task ExcluirComentariosAutor(string autorId)
        {
            var comentarios = await _context.Comentarios.Where(c => c.AutorId == autorId).ToListAsync();
            _context.Comentarios.RemoveRange(comentarios);
        }

        public async Task<Comentario> ObterPorId(int id)
        {
           return await _context.Comentarios.FindAsync(id);
        }
    }
}
