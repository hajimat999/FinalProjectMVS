using FinalProject.Models.Base;

namespace FinalProject.Models
{
    public class ClothingSize : BaseEntity
    {
        public Size Size { get; set; }
        public Clothing Clothing { get; set; }
        public int SizeId { get; set; }
        public int ClothingId { get; set; }
        public int Count { get; set; }
    }
}
