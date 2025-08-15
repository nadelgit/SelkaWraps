using System.ComponentModel.DataAnnotations;

namespace SelkaWraps.Data
{
    public class Buyer
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
