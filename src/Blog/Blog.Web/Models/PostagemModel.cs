using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class PostagemModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]        
        public string Titulo { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(1, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        public string Conteudo { get; set; }        
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
        [Required]
        public AutorModel Autor { get; set; }
        public IEnumerable<ComentarioModel> Comentarios { get; set; }
    }
}
