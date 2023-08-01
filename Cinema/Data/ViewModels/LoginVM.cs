using System.ComponentModel.DataAnnotations;

namespace AppCinema.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email adress")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
