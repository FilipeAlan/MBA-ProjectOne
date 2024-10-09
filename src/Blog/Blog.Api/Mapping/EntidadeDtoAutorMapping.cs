using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;

namespace Blog.Web.Mapping
{
    public class EntidadeDtoAutorMapping:Profile
    {
        public EntidadeDtoAutorMapping()
        {
            CreateMap<Autor, AutorDto>().ReverseMap();
        }
    }
}
