using AutoMapper;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class PostagemController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PostagemController(IPostagemRepositorio postagemRepositorio, IMapper mapper , UserManager<IdentityUser> userManager)
        {
            _postagemRepositorio = postagemRepositorio;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Exibe o formulário para criar uma nova postagem
        public IActionResult Criar()
        {
            return View("Criar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(PostagemModel postagemModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postagemModel);
            }
            
            var user = await _userManager.GetUserAsync(User);
                        
            if (user == null)
            {
                return Unauthorized();
            }

            var postagem = _mapper.Map<Postagem>(postagemModel);

            postagem.AutorId = user.Id;

            await _postagemRepositorio.Adicionar(postagem);

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Editar(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound();
            }
            var postagemModel = _mapper.Map<PostagemModel>(postagem);
            return View(postagemModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([Bind("Id,Titulo,Conteudo,AutorId")] PostagemModel postagemModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", postagemModel);
            }
            var postagem = await _postagemRepositorio.ObterPorId(postagemModel.Id);
            
            if (postagem == null)
            {
                return NotFound();
            }

            postagem.Titulo = postagemModel.Titulo;
            postagem.Conteudo = postagemModel.Conteudo;

            // Chama o repositório para atualizar a postagem
            await _postagemRepositorio.Atualizar(postagem);

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound();
            }

            await _postagemRepositorio.Deletar(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);
            if (postagem == null)
            {
                return NotFound();             }

            return View(postagem); 
        }

    }
}
