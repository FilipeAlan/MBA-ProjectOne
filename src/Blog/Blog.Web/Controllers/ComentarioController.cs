using AutoMapper;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Criar(ComentarioModel comentarioModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comentario = _mapper.Map<Comentario>(comentarioModel);
            comentario.AutorId = autorId;
            comentario.DataPublicacao = DateTime.Now;

            await _comentarioRepositorio.Adicionar(comentario);

            return RedirectToAction("Index", "Home", new { id = comentarioModel.PostagemId });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var comentario = await _comentarioRepositorio.ObterPorId(id);

            if (comentario == null)
            {
                return View("Limbo");
            }
            
            var comentarioModel = _mapper.Map<ComentarioModel>(comentario);

            return View(comentarioModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ComentarioModel comentarioModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var comentario = await _comentarioRepositorio.ObterPorId(comentarioModel.Id);

            if (comentario == null)
            {
                return NotFound();
            }

            comentario.Conteudo = comentarioModel.Conteudo;

            await _comentarioRepositorio.Atualizar(comentario);
            
            return RedirectToAction("Index", "Home", new { id = comentarioModel.PostagemId });
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

            return RedirectToAction("Index", "Home");
        }
    }


}
