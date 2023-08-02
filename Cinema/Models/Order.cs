using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCinema.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        //Connected to table the ApplicationUser, for table the Order
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
