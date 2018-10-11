// The View Model
// The Model is a single item in the database
// The View Model might need to display two, ten or a hundred todo-items
// So here it is an array as a data structure
namespace AspNetCoreTodo.Models
{
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
    }
}
