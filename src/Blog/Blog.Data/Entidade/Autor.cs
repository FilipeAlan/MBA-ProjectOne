using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entidade
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IEnumerable<Postagem> Postagens { get; set; }
        
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
