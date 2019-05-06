using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Data
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }

    }
}
