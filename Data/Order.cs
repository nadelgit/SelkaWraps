using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelkaWraps.Data
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;

        [Required]
        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}
