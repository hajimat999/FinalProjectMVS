using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.HomeVMs
{
    public class LayoutBasketVM
    {
        public decimal TotalPrice { get; set; }
        public List<BasketItemVM> BasketItemVMs { get; set; }
        
    }
}
