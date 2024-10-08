using Blog.Data.Entidade;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class AlunoDto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }        
        public IEnumerable<PostagemDto> Postagens { get; set; }
    }
}
