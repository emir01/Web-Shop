(function () {
    "use strict";

    angular
        .module("ws.layout")
        .controller("NavController", NavController);

    NavController.$inject = ["common", "services"];

    function NavController(common, services) {
        var vm = this;

        vm.Logout = logOut;
        vm.Title = "Web Shop";

        activate();

        function activate() { }

        function logOut() {
            services.auth.Logout();
            common.$state.go("products.search", { reload: true });
        }
    }
})();