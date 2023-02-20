using FinalProject.Models;
using System.Collections.Generic;

namespace FinalProject.HomeVMs
{
    public class LayoutBasketVM2
    {
        public decimal TotalPrice { get; set; }
        public List<BasketItemVM> BasketItemVMs { get; set; }
    }
}
