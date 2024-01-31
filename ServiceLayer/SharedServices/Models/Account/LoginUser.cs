using System.ComponentModel.DataAnnotations;

namespace SharedServices.Models.Account
{
    public record LoginUser()
    {
        [EmailAddress, Required]
        public string? Email { get; set; }
        [DataType(DataType.Password), Required]
        public string? Password { get; set; }
    }
}
