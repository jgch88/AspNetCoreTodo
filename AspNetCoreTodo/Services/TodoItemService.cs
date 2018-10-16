using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            // get database.Items.Where(Item.IsDone)
            // Where method is a feature of C# called LINQ (Language INtegrated Query)
            // Entity Framework Core translates the Where method into a statement like
            // SELECT * FROM Items where IsDone = 0, or some NoSQL equivalent

            // .ToArraySync() gets all entities that match the filter, and returns them 
            // as an array.
            return await _context.Items
                .Where(x => x.IsDone == false) // this is like js syntax
                .ToArrayAsync();
        }
    }
}
