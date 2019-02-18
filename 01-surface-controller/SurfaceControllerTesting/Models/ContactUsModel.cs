using System.ComponentModel.DataAnnotations;

namespace SurfaceControllerTesting.Models
{
    public class ContactUsModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}