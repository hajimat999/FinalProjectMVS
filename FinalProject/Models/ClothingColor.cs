using FinalProject.Models.Base;

namespace FinalProject.Models
{
    public class ClothingColor : BaseEntity
    {
        public Color Color { get; set; }
        public Clothing Clothing { get; set; }
        public int ColorId { get; set; }
        public int ClothingId { get; set; }
        public int Count { get; set; }
    }
}
