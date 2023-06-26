using AppCinema.Data.Base;
using AppCinema.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCinema.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set;}
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MovieCategory MovieCategory { get; set; }



        ////////////////////////////////////////////////////////////////////
        //Relationships "Encji":

        /******************************************************************/
        //One - to - Many:


        //Cinema
        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; }
        public Cinema  Cinema { get; set; } //several Movies - can have -  one Cinema



        //Producer
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; } //several Movies - can have -  one Producer


        /******************************************************************/
        //Many - to - Many:


        public List<Actor_Movie> Actors_Movies { get; set; } //several Movies - can have -  several Actors

    }
}
