
using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;
using Blog.Data.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly UserManager<Autor> _userManager;
        private readonly SignInManager<Autor> _signInManager;

        public AutorController(UserManager<Autor> userManager, SignInManager<Autor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AutorDto autorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var user = await _userManager.FindByEmailAsync(autorDto.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." }); 
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, autorDto.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }

            var result = await _signInManager.PasswordSignInAsync(user, autorDto.Password, autorDto.RememberMe, true);
            if (result.Succeeded)
            {                
                var token = GenerateJwtToken(user);
                                
                return Ok(new { token });
            }            

            return BadRequest(new { message = "Tentativa de login inválida." });
        }
        
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] AutorRegistroDto autorRegistroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var user = new Autor { UserName = autorRegistroDto.Email, Email = autorRegistroDto.Email, Nome = autorRegistroDto.Nome };
            var result = await _userManager.CreateAsync(user, autorRegistroDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Nome", user.Nome));                
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new { id = user.Id, email = user.Email, nome = user.Nome });
            }
            
            return BadRequest(result.Errors);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }

            return Ok(new { id = user.Id, email = user.Email, nome = user.Nome });
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout realizado com sucesso." });
        }
        
        // No MCV não precisei aqui tive que gerar. Mas não tratei passando em todos os métodos.
        private string GenerateJwtToken(Autor user)
        {
            return GeradorToken.GerarToken(user);
        }
    }
}
