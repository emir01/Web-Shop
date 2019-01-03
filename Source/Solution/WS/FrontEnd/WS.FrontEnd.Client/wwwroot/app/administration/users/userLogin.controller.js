(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("LoginController", LoginController);

    LoginController.$inject = ["common", "services"];

    function LoginController(common, services) {
        var vm = this;

        vm.loginInfo = {
            userName: "",
            password: ""
        };

        vm.login = _login;

        activate();

        function activate() {}

        function _login() {
            services.auth.Login(vm.loginInfo)
                .then(successLogin, failedLogin);

            function successLogin() {
                var redirectedFrom = common.$state.params.previous;

                if (redirectedFrom) {
                    common.$state.go(redirectedFrom);
                } else {
                    common.$state.go(common.defaults.state);
                }
            }

            function failedLogin(message) {
                common.logger.error(message, message, "Login Failed");
            }
        }
    };
})();