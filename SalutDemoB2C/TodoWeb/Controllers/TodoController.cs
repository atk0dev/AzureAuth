using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using TodoWeb.Models;
using TodoWeb.Services;

namespace TodoWeb.Controllers
{
    public class TodoController : Controller
    {
        private ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [AuthorizeForScopes(ScopeKeySection = "Todo:TodoScope")]
        public async Task<ActionResult> Index()
        {
            return View(await _todoService.GetAsync());
        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await _todoService.GetAsync(id));
        }

        public ActionResult Create()
        {
            Todo todo = new Todo() { Owner = HttpContext.User.Identity.Name };
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,Owner")] Todo todo)
        {
            await _todoService.AddAsync(todo);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            Todo todo = await this._todoService.GetAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Title,Owner")] Todo todo)
        {
            await _todoService.EditAsync(todo);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            Todo todo = await this._todoService.GetAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, [Bind("Id,Title,Owner")] Todo todo)
        {
            await _todoService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}