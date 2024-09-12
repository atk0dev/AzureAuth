using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [RequiredScope(new[] { "TodoScope" })]
    public class TodoController : Controller
    {
        private static readonly Dictionary<int, Todo> TodoStore = new Dictionary<int, Todo>();

        private readonly IHttpContextAccessor _contextAccessor;
     
        public TodoController(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;

            if (TodoStore.Count == 0)
            {
                TodoStore.Add(1, new Todo() { Id = 1, Owner = $"{this._contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name").Value}", Title = "Pick up groceries" });
                TodoStore.Add(2, new Todo() { Id = 2, Owner = $"{this._contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name").Value}", Title = "Finish invoice report" });
            }
        }

        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            string owner = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            return TodoStore.Values.Where(x => x.Owner == owner);
        }

        [HttpGet("{id}", Name = "Get")]
        public Todo Get(int id)
        {
            return TodoStore.Values.FirstOrDefault(t => t.Id == id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TodoStore.Remove(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Todo todo)
        {
            int id = TodoStore.Values.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            Todo todonew = new Todo() { Id = id, Owner = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name").Value, Title = todo.Title };
            TodoStore.Add(id, todonew);

            return Ok(todo);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (TodoStore.Values.FirstOrDefault(x => x.Id == id) == null)
            {
                return NotFound();
            }

            TodoStore.Remove(id);
            TodoStore.Add(id, todo);

            return Ok(todo);
        }
    }
}