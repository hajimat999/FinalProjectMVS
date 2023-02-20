using FinalProject.Models.Base;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Size:BaseEntity
    {
        public string SizeName { get; set; }
        public List<ClothingSize> ClothingSizes { get; set; }
    }
}
