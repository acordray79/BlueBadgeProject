using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Models.Customer
{
    public class CustomerListItem
    {
        public int CustomerID { get; set; }
        
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CreditCard { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public override string ToString() => CustomerName;
    }
}
