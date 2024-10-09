using AutoMapper;
using Blog.Data.Entidade;
using Blog.Web.Models;
namespace Blog.Web.Mapping
{
    public class EntidadeModelAutorMapping:Profile
    {
        public EntidadeModelAutorMapping()
        {
            CreateMap<Autor, AutorModel>().ReverseMap();
        }
    }
}
