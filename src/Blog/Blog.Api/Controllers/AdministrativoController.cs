using Blog.Api.Dto;
using Blog.Data.Entidade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministrativoController : ControllerBase
    {
        private readonly UserManager<Autor> _userManager;
        public AdministrativoController(UserManager<Autor> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var users = await _userManager.Users.ToListAsync(); // Realizando a operação de maneira assíncrona

            var model = new List<AutorRegistroDto>();

            foreach (var user in users)
            {
                var ehAdmin = await _userManager.IsInRoleAsync(user, "Administrador");
                model.Add(new AutorRegistroDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    EhAdmin = ehAdmin
                });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAdminRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            string message;
            bool isAdmin;

            if (await _userManager.IsInRoleAsync(user, "Administrador"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrador");
                message = "Role Administrador removida";
                isAdmin = false;
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
                message = "Role Administrador adicionada";
                isAdmin = true;
            }

            return Ok(new
            {
                userId = user.Id,
                email = user.Email,
                isAdmin,
                message
            });
        }


        [HttpPost("criar-usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] AutorRegistroDto autorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Autor
            {
                UserName = autorDto.Email,
                Email = autorDto.Email,
                Nome = autorDto.Nome
            };

            var result = await _userManager.CreateAsync(user, autorDto.Password);

            if (result.Succeeded)
            {
                if (autorDto.EhAdmin)
                {
                    await _userManager.AddToRoleAsync(user, "Administrador");
                }
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
