using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Authentic.Models
{
    public class Usuario //: IdentityUser
    {
         public Guid Id { get; set; }
        // public string UserName { get; set; }
        // public string Email { get; set; }
        // public string Password { get; set; }
        // public string Role { get; set; }
    }
}