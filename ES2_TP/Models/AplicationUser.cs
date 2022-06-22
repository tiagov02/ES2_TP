namespace ES2_TP.Models
{
    public class AplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
