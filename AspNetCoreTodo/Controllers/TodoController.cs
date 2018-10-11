using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        // private variable to hold reference to ITodoItemService
        // Lets you use the service from the Index action method
        private readonly ITodoItemService _todoItemService;

        // TodoController constructor fills the private variable
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }


        public async Task<IActionResult> Index()
        {
            // Get to-do items from database
            var items = await _todoItemService.GetIncompleteItemsAsync();
            // Put items into a model
            var model = new TodoViewModel()
            {
                Items = items
            };
            // Render view using the model
            return View(model);
        }
    }
}