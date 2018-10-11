using System;
using System.ComponentModel.DataAnnotations;

// The Model in the database itself - not the Model used in the app which is a View Model
// Model, Entity
namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        // guid: globally unique identifier

        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }
        // C# type that stores a date/time stamp with timezone offset from UTC
        // ? marks that it is nullable or optional
    }
}
