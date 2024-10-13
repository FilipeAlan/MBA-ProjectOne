using AutoMapper;
using Blog.Data.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IMapper _mapper;
        
        public HomeController(ILogger<HomeController> logger,IPostagemRepositorio postagemRepositorio,IMapper mapper )
        {
            _postagemRepositorio = postagemRepositorio;
            _mapper = mapper;
            _logger = logger;
        }        

        public async Task<IActionResult> Index()
        {
            var postagens = await _postagemRepositorio.ObterTodas();
            var postagemModels = _mapper.Map<IEnumerable<PostagemModel>>(postagens);

            return View(postagemModels);
        }

        public IActionResult Limbo()
        {
            return View("Limbo");
        }
    }
}
