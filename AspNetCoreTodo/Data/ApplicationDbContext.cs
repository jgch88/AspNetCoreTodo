using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Store TodoItem entities in a TABLE called Items. 
        // Adding this requires migration of database to reflect the new changes (Page 52).
        public DbSet<TodoItem> Items { get; set; }
    }
}
