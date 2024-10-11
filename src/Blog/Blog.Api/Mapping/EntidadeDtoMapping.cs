using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;

namespace Blog.Web.Mapping
{
    public class EntidadeDtoMapping:Profile
    {
        public EntidadeDtoMapping()
        {
            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<Autor, AutorRegistroDto>().ReverseMap();
            CreateMap<Postagem, PostagemDto>().ReverseMap();
            CreateMap<Comentario, ComentarioDto>().ReverseMap();            
        }
    }
}
