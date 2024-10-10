using AutoMapper;
using Blog.Data.Entidade;
using Blog.Web.Models;
namespace Blog.Web.Mapping
{
    public class EntidadeModelMapping:Profile
    {
        public EntidadeModelMapping()
        {
            CreateMap<Autor, AutorModel>().ReverseMap();
            CreateMap<Postagem, PostagemModel>().ReverseMap();
            CreateMap<Comentario, ComentarioModel>().ReverseMap();
            CreateMap<Autor, RegisterModel>().ReverseMap();
        }
    }
}
