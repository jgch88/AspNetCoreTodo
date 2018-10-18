using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Models
{
    public class ManageUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }
        public IdentityUser[] Everyone { get; set; }
    }
}
