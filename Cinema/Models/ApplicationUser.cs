using Microsoft.AspNetCore.Identity; // for IdentityUser
using System.ComponentModel.DataAnnotations;

namespace AppCinema.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
    }
}
