using Blog.Data.Entidade;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class ComentarioDto
    {
        public int Id { get; set; }        
        public string Nome { get; set; }        
        public string Email { get; set; }
        [Required]
        public string Conteudo { get; set; }        
    }
}
