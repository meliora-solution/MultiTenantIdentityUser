using System.ComponentModel.DataAnnotations;

namespace SharedServices.Models.EasyStock
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Contact Type  is required")]
        public string type { get; set; }
        [Required(ErrorMessage = "Name  is required")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Datakey { get; set; } 
        public string? Description { get; set; }
    }
}
