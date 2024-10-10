using AutoMapper;
using Blog.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class ComentarioController : Controller
    {
        private readonly IComentarioRepositorio _comentarioRepositorio;
        private readonly IMapper _mapper;
        public ComentarioController(IComentarioRepositorio comentarioRepositorio,IMapper mapper)
        {
            _comentarioRepositorio = comentarioRepositorio;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var comentario = await _comentarioRepositorio.ObterPorId(id);

            if (comentario == null)
            {
                return NotFound();
            }

            await _comentarioRepositorio.Deletar(comentario);
            
            return Ok();
        }
    }

}
