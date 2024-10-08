using Blog.Data.Entidade;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class AutorDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(2, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email inválido.")]
        public string Email { get; set; }        
        public IEnumerable<PostagemDto> Postagens { get; set; }
    }
}
