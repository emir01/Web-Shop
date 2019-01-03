using System.ComponentModel.DataAnnotations;

namespace WS.FrontEnd.WebApi.Authentication.BindingsModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}