using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        // private variable to hold reference to ITodoItemService
        // Lets you use the service from the Index action method
        private readonly ITodoItemService _todoItemService;

        private readonly UserManager<IdentityUser> _userManager;

        // TodoController constructor fills the private variable
        public TodoController(ITodoItemService todoItemService,
            UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            // Get user using the UserManager to look up the User property available in the action.
            var currentUser = await _userManager.GetUserAsync(User);

            // currentUser technically should never be [null] because of [Authorize] above.
            // This is just a sanity check. Challenge() forces the user to login again
            // if info is missing.
            if (currentUser == null) return Challenge();

            // Get to-do items from database
            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);
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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService
                .AddItemAsync(newItem, currentUser);
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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService
                .MarkDoneAsync(id, currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}