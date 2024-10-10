using AutoMapper;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAutorRepositorio _autorRepositorio;
        private readonly IMapper _mapper;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAutorRepositorio autorRepositorio,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _autorRepositorio = autorRepositorio;
            _mapper = mapper;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Login inválido.");
            return View(loginModel);
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel RegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return View(RegisterModel);
            }

            var user = new IdentityUser { UserName = RegisterModel.Email, Email = RegisterModel.Email };
            var result = await _userManager.CreateAsync(user, RegisterModel.Password);

            if (result.Succeeded)
            {
                var autor = _mapper.Map<RegisterModel, Autor>(RegisterModel);
                await _autorRepositorio.Adicionar(autor);
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(RegisterModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
