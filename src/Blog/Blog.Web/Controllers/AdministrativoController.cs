using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Data.Repositorio;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministrativoController : Controller
    {
        private readonly UserManager<Autor> _userManager;
        private readonly IComentarioRepositorio _comentarioRepositorio;
        public AdministrativoController(UserManager<Autor> userManager, RoleManager<IdentityRole> roleManager,IComentarioRepositorio comentarioRepositorio)
        {
            _userManager = userManager;
            _comentarioRepositorio = comentarioRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();            

            var model = new List<AutorRegistrarModel>();

            foreach (var user in users)
            {
                var ehAdmin = await _userManager.IsInRoleAsync(user, "Administrador");
                model.Add(new AutorRegistrarModel
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    EhAdmin = ehAdmin
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("Limbo");
            }

            await _comentarioRepositorio.ExcluirComentariosAutor(id);

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> ToggleAdminRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("Limbo");
            }

            if (await _userManager.IsInRoleAsync(user, "Administrador"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrador");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(AutorRegistrarModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var user = new Autor
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded && model.EhAdmin)
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
            }

            return RedirectToAction("Index");
        }
    }


}
