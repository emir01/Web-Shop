(function () {
    "use strict";

    angular
        .module("blocks.authorization")
        .factory("stateAuthorizationConfiguration", stateAuthorizationConfiguration);

    stateAuthorizationConfiguration.$inject = ["common", "services"];

    function stateAuthorizationConfiguration(common, services) {
        var factory = {};

        factory.initialize = initialize;

        return factory;

        function initialize() {
            common.$rootScope.$on('$stateChangeStart', function (event, toState) {
                var stateData = toState.data;

                if (stateData != undefined && stateData.auth) {
                    if (!services.auth.IsAuthorized()) {
                        common.logger.error("You must be authorized to access " + stateData.name, "Authorization Failed");
                        event.preventDefault();
                        common.$state.go("admin.login", { previous: toState.name });
                    }
                }
            });
        }
    }
})();