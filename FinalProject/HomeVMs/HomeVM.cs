using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.HomeVMs
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Clothing> Clothes { get; set; }
        public List<Category> Categories { get; set; }
       
    }
}
