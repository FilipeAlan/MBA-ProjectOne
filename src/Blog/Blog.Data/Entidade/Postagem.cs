namespace Blog.Data.Entidade
{
    public class Postagem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public Autor Autor { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }    
    }
}
