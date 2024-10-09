using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string BillingAddress { get; set; }
        public string DefaultShipingAddress { get; set; }
        public string Country { get; set; }


    }
}
