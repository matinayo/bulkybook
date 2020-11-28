using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Range(1, 10000, ErrorMessage = "Please enter a message between 1 and 100")]
        public int Count { get; set; }
        [NotMapped]
        public double Price { get; set; }
        // if less than 25, we would have a price
        // more than 50 would have different price

        // i.e. * based on the count selected here, we would be loading the price so that we can
        // display on the ui
    }
}
