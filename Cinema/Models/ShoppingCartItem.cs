using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace AppCinema.Models
{
    public class ShoppingCartItem
    {   
        [Key]
        public int Id { get; set; }

        public Movie Movie { get; set; }
        
        public int Amout { get; set; }


        public string ShoppingCartId { get; set; }
    }
}
