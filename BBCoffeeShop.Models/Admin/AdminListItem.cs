﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Models.Admin
{
    public class AdminListItem
    {
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public override string ToString() => AdminName;
    }
}
