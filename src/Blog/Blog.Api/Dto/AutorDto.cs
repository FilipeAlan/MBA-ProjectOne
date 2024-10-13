using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Dto
{
    public class AutorDto
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Nome { get; set; }
        public bool RememberMe { get; set; }

    }
}
