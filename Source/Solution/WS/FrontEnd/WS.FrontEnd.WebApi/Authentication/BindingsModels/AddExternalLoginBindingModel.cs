using System.ComponentModel.DataAnnotations;

namespace WS.FrontEnd.WebApi.Authentication.BindingsModels
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}