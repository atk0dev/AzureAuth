using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWeb.Models;

namespace TodoWeb.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAsync();

        Task<Todo> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<Todo> AddAsync(Todo todo);

        Task<Todo> EditAsync(Todo todo);
    }
}
