using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

// Architecture of this app is split into 2:
// Presentation layer and Service layer

// Service layer here actually combines business logic and database code
namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        // TodoItem reference requires the View Model reference above
        Task<TodoItem[]> GetIncompleteItemsAsync();
        // Task type is similar to a promise
    }
}
