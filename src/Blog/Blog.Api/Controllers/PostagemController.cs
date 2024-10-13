using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IMapper _mapper;
        private readonly UserManager<Autor> _userManager;

        public PostagemController(IPostagemRepositorio postagemRepositorio, IMapper mapper, UserManager<Autor> userManager)
        {
            _postagemRepositorio = postagemRepositorio;
            _mapper = mapper;
            _userManager = userManager;
        }
                
        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] PostagemDto postagemModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "Usuário não autenticado" });
            }

            var postagem = _mapper.Map<Postagem>(postagemModel);
            postagem.AutorId = user.Id;

            await _postagemRepositorio.Adicionar(postagem);
                        
            return CreatedAtAction(nameof(ObterPorId), new { id = postagem.Id }, postagem);
        }
                
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound(new { message = "Postagem não encontrada" });
            }

            var postagemModel = _mapper.Map<PostagemDto>(postagem);
            return Ok(postagemModel);
        }
        
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] PostagemDto postagemModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound(new { message = "Postagem não encontrada" });
            }

            postagem.Titulo = postagemModel.Titulo;
            postagem.Conteudo = postagemModel.Conteudo;

            await _postagemRepositorio.Atualizar(postagem);

            return Ok(new { message = "Postagem atualizada com sucesso" });
        }
        
        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound(new { message = "Postagem não encontrada" });
            }

            await _postagemRepositorio.Deletar(id);

            return Ok(new { message = "Postagem excluída com sucesso" });
        }
        
        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound(new { message = "Postagem não encontrada" });
            }

            var postagemModel = _mapper.Map<PostagemDto>(postagem);
            return Ok(postagemModel);
        }

        //Fica na home daa  web
        [HttpGet("listar")] 
        public async Task<IActionResult> Listar()
        {
            var postagens = await _postagemRepositorio.ObterTodas();
            var postagensModel = _mapper.Map<IEnumerable<PostagemDto>>(postagens);
            return Ok(postagensModel);
        }
    }

}
