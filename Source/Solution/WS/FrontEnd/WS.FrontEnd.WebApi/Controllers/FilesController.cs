using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WS.FrontEnd.WebApi.Infrastucture.Extensions;
using WS.FrontEnd.WebApi.Infrastucture.FileManagement;

namespace WS.FrontEnd.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [Authorize]
    public class FilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            if (ActionVerbConfigService.VerbsDisabled())
            {
                ActionVerbConfigService.ThrowDisabledVerb();
                return new HttpResponseMessage();
            }

            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var storeResult =  await new FileManager().Process(Request);

            return Request.CreateResponse(HttpStatusCode.OK, storeResult);
        }
    }
}
