using System.Threading.Tasks;
using Microsoft.Owin;

namespace WS.FrontEnd.WebApi.Authentication.MIddleware
{
    public class InvalidAuthenticationMiddleware : OwinMiddleware
    {
        /// <summary>
        /// Instantiates the middleware with an optional pointer to the next component.
        /// </summary>
        /// <param name="next"/>
        public InvalidAuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }
        
        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"/>
        /// <returns/>
        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);
            
            if (context.Response.StatusCode == 400 && context.Response.Headers.ContainsKey("AuthorizationResponse"))
            {
                context.Response.Headers.Remove("AuthorizationResponse");
                context.Response.StatusCode = 401;
            }
        }
    }
}