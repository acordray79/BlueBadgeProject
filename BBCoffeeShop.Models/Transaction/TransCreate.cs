using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Models.Transaction
{
    public class TransCreate
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
