using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Models.Transaction
{
    public class TransUpdate
    {
        public int TransactionID { get; set; }

        public int CustomerID { get; set; }

        public int ProductID { get; set; }

        public string CustomerName { get; set; }
        
        public string ProductName { get; set; }
    }
}
