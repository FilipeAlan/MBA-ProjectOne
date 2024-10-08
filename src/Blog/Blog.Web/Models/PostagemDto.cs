using Blog.Data.Entidade;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class PostagemDto
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Conteudo { get; set; }        
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
        [Required]
        public Autor Autor { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
    }
}
