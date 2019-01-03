using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using WS.FrontEnd.WebApi.Authentication.BindingsModels;

namespace WS.FrontEnd.WebApi.Authentication.Repositories
{
    public class AuthRepository : IDisposable
    {
        private AuthDbContext context;

        private UserManager<ApplicationUser> userManager;

        public AuthRepository()
        {
            context = new AuthDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public async Task<IdentityResult> RegisterUser(RegisterBindingModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await userManager.FindAsync(userName, password);

            return user;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}