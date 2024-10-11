namespace Blog.Data.Entidade
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int PostagemId { get; set; }
        public Postagem Postagem { get; set; }
        public string AutorId { get; set; } 
        public Autor Autor { get; set; }  
    }
}
