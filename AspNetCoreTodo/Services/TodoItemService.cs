using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
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

        public async Task<TodoItem[]> GetIncompleteItemsAsync(
            IdentityUser user)
        {
            // get database.Items.Where(Item.IsDone)
            // Where method is a feature of C# called LINQ (Language INtegrated Query)
            // Entity Framework Core translates the Where method into a statement like
            // SELECT * FROM Items where IsDone = 0, or some NoSQL equivalent

            // .ToArraySync() gets all entities that match the filter, and returns them 
            // as an array.
            return await _context.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id) // this is like js syntax
                .ToArrayAsync();
        }

        public async Task<bool> AddItemAsync(
            TodoItem newItem, IdentityUser user)
        {
            // the remaining newItem properties except "Title"
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            newItem.UserId = user.Id;

            _context.Items.Add(newItem);

            // probably an inherited DB method
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync(); // .SingleOrDefaultAsync() will either return the item or null

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
