using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.HomeVMs
{
    public class ClothesCategory
    {
        public Clothing Cloth { get; set; }
        public List<Clothing> Clothes { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        
    }
}
