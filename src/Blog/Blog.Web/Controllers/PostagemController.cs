using AutoMapper;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Data.Repositorio;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class PostagemController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IMapper _mapper;

        public PostagemController(IPostagemRepositorio postagemRepositorio, IMapper mapper)
        {
            _postagemRepositorio = postagemRepositorio;
            _mapper = mapper;
        }

        // GET: Exibe o formulário para criar uma nova postagem
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Cria uma nova postagem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Conteudo,Autor")] PostagemModel postagemModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", postagemModel); 
            }

            var postagem = _mapper.Map<Postagem>(postagemModel);
            await _postagemRepositorio.Adicionar(postagem);
            
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Detalhes(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);

            if (postagem == null)
            {
                return NotFound(); 
            }

            return PartialView("_Detalhe", postagem);
        }
        
        public async Task<IActionResult> Editar(int id)
        {
            var postagem = await _postagemRepositorio.ObterPorId(id);

            if (postagem == null)
            {
                return NotFound();
            }

            var postagemModel = _mapper.Map<PostagemModel>(postagem);
            return View("Edit", postagemModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([Bind("Id,Titulo,Conteudo,AutorId")] PostagemModel postagemModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", postagemModel);
            }

            var postagem = _mapper.Map<Postagem>(postagemModel);
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

            await _postagemRepositorio.Deletar(postagem);
          
            return RedirectToAction("Index", "Home");
        }
    }
}
