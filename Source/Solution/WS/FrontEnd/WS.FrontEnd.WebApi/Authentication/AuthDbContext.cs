using Microsoft.AspNet.Identity.EntityFramework;

namespace WS.FrontEnd.WebApi.Authentication
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext()
            : base("WsContextConnectionString")
        {
        }
        
        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }
    }
}