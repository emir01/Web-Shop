using System.Net;
using System.Net.Http;
using WS.Logic.Core.Results;

namespace WS.FrontEnd.WebApi.Infrastucture
{
    public static class ResponseMessageBuilder
    {
        public static HttpResponseMessage BuildMessageFromActionResult<T>(ActionResult<T> actionResult)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(actionResult.Message)
            };
        }
    }
}