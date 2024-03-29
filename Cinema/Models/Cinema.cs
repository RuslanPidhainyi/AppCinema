﻿using AppCinema.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCinema.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Cinema logo")]
        [Required(ErrorMessage = "Logo is required")]
        public string Logo { get; set; }


        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 chars")]
        public string Name { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }



        ////////////////////////////////////////////////////////////////////
        //Relationships "Encji":


        /******************************************************************/
        //One - to - Many:
   
        public List<Movie> Movies { get; set; } //one Cinema - can have -  several Movies
    }
}
