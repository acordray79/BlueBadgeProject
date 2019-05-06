using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Data
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public bool Purchase { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        public virtual string CustomerName { get; set; }
        public virtual string ProductName { get; set; }

    }
}
