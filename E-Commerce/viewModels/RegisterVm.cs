using System.ComponentModel.DataAnnotations;

namespace E_Commerce.viewModels
{
    public class RegisterVm
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public  string Role { get; set; }
        public  string Country { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
