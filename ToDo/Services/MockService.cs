using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Services
{
    public class MockService : IAzureService
    {
        List<ToDoItem> items { get; set; } = new List<ToDoItem>();

        public MockService()
        {
            if (items.Count == 0)
                items = MockToDoItems();
        }

        public Task<ToDoItem> AddToDo(string text, bool complete)
        {
            var item = new ToDoItem
            {
                Text = text,
                Complete = complete
            };

            items.Add(item);
            return Task.FromResult(item);
        }

        public Task<ToDoItem> UpdateItem(ToDoItem item)
        {
            var todo = items.FirstOrDefault(x => x.Id == item.Id);
            items.Remove(todo);
            items.Add(item);
            return Task.FromResult(item);
        }

        public Task<bool> DeleteItem(ToDoItem item)
        {
            items.Remove(item);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<ToDoItem>> GetToDo()
        {
            IEnumerable<ToDoItem> todos = items.AsEnumerable();
            return Task.FromResult(todos);
        }

        public Task Initialize()
        {
            return null;
        }

        public Task SyncToDo()
        {
            return null;
        }

        List<ToDoItem> MockToDoItems()
        {
            var items = new List<ToDoItem>();

            var todo1 = new ToDoItem
            {
                Text = "AzP - Create a Mobile App backend: New => Web + Mobile => Mobile App",
                Complete = false
            };
            items.Add(todo1);

            var todo2 = new ToDoItem
            {
                Text = "AzP- Create a Database: Quickstart => Xamarin.Forms => Connect a database",
                Complete = true
            };
            items.Add(todo2);

            var todo3 = new ToDoItem
            {
                Text = "AzP - Create a Table API: Quickstart => Xamarin.Forms => Connect a table API",
                Complete = false
            };
            items.Add(todo3);


            var todo4 = new ToDoItem
            {
                Text = "VS - Create Azure App: VS => New Project => Web => ASP.NET Web Application => Azure Mobile App",
                Complete = false
            };


            var todo5 = new ToDoItem
            {
                Text = "VS - Public Azure App: VS => Publish => Login to AzP => Publish",
                Complete = false
            };

            items.Add(todo5);

            return items;
        }
    }

}
