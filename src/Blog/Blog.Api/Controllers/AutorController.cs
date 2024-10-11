
using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Autor> _userManager; 

        public AutorController(IMapper mapper, UserManager<Autor> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
                
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var autores = _userManager.Users.ToList(); 
            var autoresDto = _mapper.Map<IEnumerable<AutorDto>>(autores);

            return Ok(autoresDto);
        }
       
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AutorRegistroDto autorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autor = _mapper.Map<Autor>(autorDto);
            
            var result = await _userManager.CreateAsync(autor, autorDto.Password);

            if (!result.Succeeded)
            {                
                return BadRequest(result.Errors);
            }

            return Ok(autor);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] AutorDto autorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autor = await _userManager.FindByIdAsync(id);
            if (autor == null)
            {
                return NotFound("Autor não encontrado.");
            }
           
            autor.Nome = autorDto.Nome;
            autor.Email = autorDto.Email;
            autor.UserName = autorDto.Email; 

            var result = await _userManager.UpdateAsync(autor);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(autor);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var autor = await _userManager.FindByIdAsync(id);
            if (autor == null)
            {
                return NotFound("Autor não encontrado.");
            }

            var result = await _userManager.DeleteAsync(autor);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Autor excluído com sucesso.");
        }
    }
}
