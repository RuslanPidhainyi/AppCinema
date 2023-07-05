using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCinema.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amout { get; set; } //Amount - suma

        public  double Price { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]

        public  Movie Movie { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]

        public Order Order { get; set; }

    }
}
