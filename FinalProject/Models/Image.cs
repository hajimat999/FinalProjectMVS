using FinalProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public string Alternative { get; set; }
        public bool? IsMain { get; set; }
        public int ClothingId { get; set; }
        public Clothing Clothing { get; set; }
    }
}
