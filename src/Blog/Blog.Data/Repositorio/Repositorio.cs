using Blog.Data.Context;
using Blog.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly BlogDbContext _context;
        public Repositorio(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<int> Adicionar(T entidade)
        {
            _context.Add(entidade);            
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Atualizar(T entidade)
        {
            _context.Update(entidade);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Deletar(T entidade)
        {
            _context.Set<T>().Remove(entidade);
            return await _context.SaveChangesAsync();

        }
        public async Task<T> ObterPorId(int id)
        {
            return await _context.FindAsync<T>(id);
        }
        public async Task<IEnumerable<T>> ObterTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
