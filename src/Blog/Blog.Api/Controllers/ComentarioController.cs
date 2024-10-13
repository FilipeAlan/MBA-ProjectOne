using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioRepositorio _comentarioRepositorio;
        private readonly IMapper _mapper;

        public ComentarioController(IComentarioRepositorio comentarioRepositorio, IMapper mapper)
        {
            _comentarioRepositorio = comentarioRepositorio;
            _mapper = mapper;
        }
        
        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] ComentarioDto comentarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (autorId == null)
            {
                return Unauthorized();
            }

            var comentario = _mapper.Map<Comentario>(comentarioDto);
            comentario.AutorId = autorId;
            comentario.DataPublicacao = DateTime.Now;

            await _comentarioRepositorio.Adicionar(comentario);
            
            return CreatedAtAction(nameof(ObterPorId), new { id = comentario.Id }, comentario);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var comentario = await _comentarioRepositorio.ObterPorId(id);

            if (comentario == null)
            {
                return NotFound(new { message = "Comentário não encontrado" });
            }

            var comentarioDto = _mapper.Map<ComentarioDto>(comentario);

            return Ok(comentarioDto);
        }
        
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] ComentarioDto comentarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comentario = await _comentarioRepositorio.ObterPorId(id);

            if (comentario == null)
            {
                return NotFound(new { message = "Comentário não encontrado" });
            }

            comentario.Conteudo = comentarioDto.Conteudo;

            await _comentarioRepositorio.Atualizar(comentario);

            return Ok(new { message = "Comentário atualizado com sucesso" });
        }
        
        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var comentario = await _comentarioRepositorio.ObterPorId(id);

            if (comentario == null)
            {
                return NotFound(new { message = "Comentário não encontrado" });
            }

            await _comentarioRepositorio.Deletar(comentario);

            return Ok(new { message = "Comentário excluído com sucesso" });
        }
    }

}
