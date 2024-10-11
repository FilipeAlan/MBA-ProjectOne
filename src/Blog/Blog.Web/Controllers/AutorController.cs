using AutoMapper;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    public class AutorController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AutorController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;            
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")]AutorModel autorModel)
        {
            if (!ModelState.IsValid)
            {
                return View(autorModel);
            }

            var result = await _signInManager.PasswordSignInAsync(autorModel.Email, autorModel.Password, autorModel.RememberMe, false);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            return View(autorModel);
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(AutorRegistrarModel autorRegistrarModel)
        {
            if (!ModelState.IsValid)
            {
                return View(autorRegistrarModel);
            }
           
            var user = new Autor { UserName = autorRegistrarModel.Email, Email = autorRegistrarModel.Email, Nome = autorRegistrarModel.Nome };
            var result = await _userManager.CreateAsync(user, autorRegistrarModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Nome", user.Nome));
                return RedirectToAction("Login", "Autor");
            }
           
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(autorRegistrarModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
