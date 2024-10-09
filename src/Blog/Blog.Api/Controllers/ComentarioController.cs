using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class ComentarioController : Controller
    {
        // GET: ComentarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ComentarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComentarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComentarioController/Create
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

        // GET: ComentarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComentarioController/Edit/5
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

        // GET: ComentarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComentarioController/Delete/5
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
