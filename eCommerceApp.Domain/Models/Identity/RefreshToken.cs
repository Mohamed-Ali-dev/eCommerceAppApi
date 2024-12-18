using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Models.Identity
{
    
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiredOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiredOn;
        public DateTime CreatedOn { get; set; }
        public bool IsActive => !IsExpired;
        public string UserId { get; set; }

    }
}
