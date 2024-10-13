using Blog.Data.Entidade;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{    
    public class AutorController : Controller
    {
        private readonly UserManager<Autor> _userManager;
        private readonly SignInManager<Autor> _signInManager;

        public AutorController(UserManager<Autor> userManager, SignInManager<Autor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;            
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] AutorModel autorModel)
        {
            if (!ModelState.IsValid)
            {
                return View(autorModel);
            }

            var user = await _userManager.FindByEmailAsync(autorModel.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(autorModel);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, autorModel.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(autorModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, autorModel.Password, autorModel.RememberMe, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Conta bloqueada. Tente novamente mais tarde.");
                return View(autorModel);
            }

            ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            return View(autorModel);
        }        

        [HttpGet]
        public IActionResult Registrar(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl; 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(AutorRegistrarModel autorRegistrarModel, string returnUrl = null)
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

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl); 
                }
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
