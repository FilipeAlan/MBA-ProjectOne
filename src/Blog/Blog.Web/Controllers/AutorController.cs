using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorRepositorio _autorRepositorio;
        public AutorController(IAutorRepositorio autorRepositorio)
        {
            _autorRepositorio = autorRepositorio;
        }

        // GET: AutorController
        public async Task<IActionResult> Index()
        {           
                var autores = await _autorRepositorio.ObterTodos();
                var autoresDto = autores.Select(autor => new AutorDto
                {
                    Id = autor.Id,
                    Nome = autor.Nome,
                    Email = autor.Email
                }).ToList();

                return View(autoresDto);           
        }        

        // GET: AutorController/Create
        public async Task<IActionResult> Create(AutorDto autorDto)
        {
            if (ModelState.IsValid)
            {                
                var autor = new Autor
                {
                    Nome = autorDto.Nome,
                    Email = autorDto.Email
                };
                _autorRepositorio.Adicionar(autor);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: AutorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // GET: AutorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _autorRepositorio.Deletar(id);
            return View();
        }
    }
}
