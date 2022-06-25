namespace ES2_TP.Models
{
    public class AplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public int UserType { get; set; }

        

        //public string nome { get; set; }  
        //public string pais { get; set; }
        //public string telefone { get; set; }
    }
}
