using FinalProject.Models.Base;
using Org.BouncyCastle.Asn1.X509;

namespace FinalProject.Models
{
    public class BasketItem:BaseEntity
    {
        public int ColorId { get; set; }
        public Clothing Clothing { get; set; }
        public int ClothingId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
    }
}
