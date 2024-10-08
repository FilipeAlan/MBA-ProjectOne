using Blog.Data.Entidade;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class PostagemDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]        
        public string Titulo { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(1, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        public string Conteudo { get; set; }        
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
        [Required]
        public Autor Autor { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
    }
}
