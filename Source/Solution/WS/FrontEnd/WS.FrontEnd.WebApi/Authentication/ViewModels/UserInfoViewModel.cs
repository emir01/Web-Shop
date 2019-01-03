namespace WS.FrontEnd.WebApi.Authentication.ViewModels
{
    // Models returned by AccountController actions.

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}
