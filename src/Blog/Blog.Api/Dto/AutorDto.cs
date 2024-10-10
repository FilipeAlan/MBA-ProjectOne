using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Dto
{
    public class AutorDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(2, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        [EmailAddress]
        public string Email { get; set; }                
    }
}
