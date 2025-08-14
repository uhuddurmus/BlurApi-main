using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlurApi.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required, Phone]
        [RegularExpression(@"^90\d{10}$", ErrorMessage = "Telefon 90 ile başlamalı ve 12 haneli olmalıdır.")]
        public string Phone { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;

        public bool Verified { get; set; } = true;

        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(1000000000, 9999999999, ErrorMessage = "Vergi numarası 10 haneli olmalıdır.")]
        public long TaxNumber { get; set; }

        public TaxAddress TaxAddress { get; set; } = new TaxAddress();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Disabled { get; set; } = false;
    }

    [Owned]
    public class TaxAddress
    {
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
    }
}
