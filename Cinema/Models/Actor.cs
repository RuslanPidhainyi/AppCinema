using AppCinema.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCinema.Models
{
    public class Actor : IEntityBase
    {
        //Mozna usunąć Klucz id tutaj. Ale jezeli nie usuniemy ten klucz, wtedy znaczenia będą poprostu przepisane.
        [Key]
        public int Id { get; set; }


        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50,MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set;  }


        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }


        ////////////////////////////////////////////////////////////////////
        //Relationships "Encji":


        /******************************************************************/
        //Many - to - Many:

        public List<Actor_Movie> Actors_Movies { get; set; } //several Actors - can have -  several Movies
    }
}
