using FinalProject.Models.Base;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public List<ClothingColor> ClothingColors { get; set; }
    }
}
