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
        public Color Color { get; set; }
        public Size Size { get; set; }
        public int ClothingId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
