using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.HomeVMs
{
    public class BasketItemVM
    {
        public Clothing Clothing { get; set; }
        public int ClothingId { get; set; }
        public int Quantity { get; set; }
    }
}
