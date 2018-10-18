using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

// Architecture of this app is split into 2:
// Presentation layer and Service layer

// Service layer here actually combines business logic and database code
namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        // TodoItem reference requires the View Model reference above
        // The book uses ApplicationUser but I think that's because the
        // auto creation of project creates an ApplicationUser : IdentityUser subclass
        // see https://msdn.microsoft.com/en-us/magazine/dn818488.aspx
        Task<TodoItem[]> GetIncompleteItemsAsync(
            IdentityUser user);
        // Task type is similar to a promise

        Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user);

        Task<bool> MarkDoneAsync(Guid id, IdentityUser user);
    }
}
