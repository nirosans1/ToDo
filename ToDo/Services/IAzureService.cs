using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Services
{
    public interface IAzureService
    {
        Task Initialize();

        Task<IEnumerable<ToDoItem>> GetToDo();

        Task<ToDoItem> AddToDo(string text, bool complete);

        Task<ToDoItem> UpdateItem(ToDoItem item);

        Task<bool> DeleteItem(ToDoItem item);

        Task SyncToDo();
    }
}
