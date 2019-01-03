using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;

namespace WS.FrontEnd.WebApi.Infrastucture.Extensions
{
    public static class ActionVerbConfigService
    {
        private static string VerbsDisabledSetting { get; } = "VerbsDisabled";
        private static string Message { get; } = "That action is currently disabled";

        public static T WrapAction<T>(Func<T> action)
        {
            if (VerbsDisabled())
            {
                ThrowDisabledVerb();
            }

            return action();
        }

        public static void ThrowDisabledVerb()
        {
            throw new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotImplemented,
                Content = new StringContent(Message)
            });
        }

        public static bool VerbsDisabled()
        {
            var setting = ConfigurationManager.AppSettings[VerbsDisabledSetting];

            if (setting.IsNullOrWhiteSpace())
            {
                return false;
            }

            return setting.ToLower() == "true";
        }
    }
}