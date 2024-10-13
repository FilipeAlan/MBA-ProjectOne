using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entidade
{
    public class Autor:IdentityUser
    {      
        public string Nome { get; set; }   
        public IEnumerable<Postagem> Postagens { get; set; }        
       
    }
}
