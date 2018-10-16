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

        // when TodoItem is used as an Action parameter, ASP.NET Core will
        // automatically perform "model binding".

        // Model binding looks at the data in a request and tries to intelligently
        // match the incoming fields with properties on the model.
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            // ModelState.IsValid refers to model validation of TodoItem passed from the form
            // It is customary to do this check right at the beginning of the action.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            // We aren't using model binding because id isn't a model.
            // There's no ModelState to check for validity.
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}