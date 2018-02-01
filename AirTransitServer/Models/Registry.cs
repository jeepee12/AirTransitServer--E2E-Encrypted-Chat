using System.ComponentModel.DataAnnotations;

namespace AirTransitServer.Models
{
    public class Registry
    {
        [Key]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PublicKey { get; set; }
    }
}
