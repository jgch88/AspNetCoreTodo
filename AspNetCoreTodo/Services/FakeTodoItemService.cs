using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

// The actual class code for the interface
namespace AspNetCoreTodo.Services
{
    // Like "implements" in Java
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            // TodoItem is the Model, I guess this is the constructor
            var item1 = new TodoItem
            {
                Title = "Learn ASP .NET Core",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            var item2 = new TodoItem
            {
                Title = "Build awesome apps",
                DueAt = DateTimeOffset.Now.AddDays(2)
            };

            // Remember, Task<> is a "promise", this is like Promise.resolve(items);
            return Task.FromResult(new[] { item1, item2 });
        }
    }
}
