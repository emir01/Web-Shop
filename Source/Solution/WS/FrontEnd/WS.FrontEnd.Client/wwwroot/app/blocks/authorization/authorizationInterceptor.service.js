(function () {
    "use strict";

    angular
        .module("blocks.authorization")
        .factory("AuthorizationInterceptor", authorizationInterceptor);

    authorizationInterceptor.$inject = ["$injector", "$q"];

    function authorizationInterceptor($injector) {
        var factory = {};

        factory.request = request;

        factory.responseError = responseError;

        return factory;

        function request(config) {
            config.headers = config.headers || {};

            var authData = $injector.get("UserAuthorization").GetAuthData();

            if (authData) {
                config.headers.Authorization = "Bearer " + authData.token;
            }

            return config;
        }

        function responseError(response) {
            if (response.status === 401) {

                $injector.get("UserAuthorization").Logout();
                var error = $injector.get("error");
                var language = $injector.get("language");

                return error
                    .build(language.general.messages.error.unathorized, language.general.title)
                    .withResponse(response, true)
                    .withRedirect("admin.login")
                    .show();
            } else {
                return $q.reject();
            }
        }
    }
})();