
using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepositorio _autorRepositorio;
        private readonly IMapper _mapper;
        public AutorController(IAutorRepositorio autorRepositorio,IMapper mapper)
        {
            _autorRepositorio = autorRepositorio;
            _mapper = mapper;
        }

        // GET: api/Autor
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var autores = await _autorRepositorio.ObterTodos();
            var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);

            return Ok(autoresDto);
        }

        // POST: api/Autor
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutorDto autorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autor = _mapper.Map<Autor>(autorDto);
            int result = await _autorRepositorio.Adicionar(autor);

            return Ok(result);
        }

        // PUT: api/Autor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AutorDto autorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autor = _mapper.Map<Autor>(autorDto);
            int result = await _autorRepositorio.Atualizar(autor);

            return Ok(result);
        }

        // DELETE: api/Autor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int result = await _autorRepositorio.Deletar(id);

            return Ok(result);
        }
    }

}
