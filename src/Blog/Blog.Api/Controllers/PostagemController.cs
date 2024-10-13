using AutoMapper;
using Blog.Api.Dto;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    public class PostagemController : Controller
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PostagemController(IComentarioRepositorio comentarioRepositorio, IPostagemRepositorio postagemRepositorio, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _postagemRepositorio = postagemRepositorio;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(AutorRegistroDto autorDto)
        {            
            try
            {
                var user = new Autor { UserName = autorDto.Email, Email = autorDto.Email, Nome = autorDto.Nome };
                await _userManager.CreateAsync(user, autorDto.Password);

                await _userManager.AddClaimAsync(user, new Claim("Nome", user.Nome));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
