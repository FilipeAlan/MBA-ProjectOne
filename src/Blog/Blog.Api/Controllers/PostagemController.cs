using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class PostagemController : Controller
    {
        // GET: PostagemController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostagemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostagemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostagemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostagemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostagemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostagemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostagemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
