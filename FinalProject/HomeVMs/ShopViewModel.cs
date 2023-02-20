using FinalProject.Models;
using System.Collections.Generic;

namespace FinalProject.HomeVMs
{
    public class ShopViewModel
    {
        public List<Clothing> Clothings { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
    }
}
