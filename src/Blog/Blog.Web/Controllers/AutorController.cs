using AutoMapper;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Mapping;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Blog.Web.Controllers
{
    public class AutorController : Controller
    {        
        private readonly IMapper _mapper;
        private readonly IAutorRepositorio _autorRepositorio;
        public AutorController(IAutorRepositorio autorRepositorio, IMapper mapper)
        {
            _autorRepositorio = autorRepositorio;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index()
        {
            var autores = await _autorRepositorio.ObterTodos();
            var autoresModel = _mapper.Map<IEnumerable<AutorModel>>(autores);

            return View("Index", autoresModel);
        }

        public async Task<IActionResult> Create(int id)
        {            
            return View();
        }        

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome,Email")] AutorModel autorModel)
        {
            if (!ModelState.IsValid)
            {   
                return View("Limbo");
            }

            var autor = _mapper.Map<Autor>(autorModel);
            await _autorRepositorio.Adicionar(autor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([Bind("Id","Nome","Email")] AutorModel autorModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Limbo");
            }
            var autor =_mapper.Map<Autor>(autorModel);
            await _autorRepositorio.Atualizar(autor);
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> Delete()
        {            
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _autorRepositorio.Deletar(id);
            return RedirectToAction("Index");
        }
    }
}
