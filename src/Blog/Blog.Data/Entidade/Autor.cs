namespace Blog.Data.Entidade
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IEnumerable<Postagem> Postagens { get; set; }
    }
}
